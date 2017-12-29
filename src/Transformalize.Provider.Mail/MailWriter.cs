﻿using System.Collections.Generic;
using System.Linq;
using Transformalize.Context;
using Transformalize.Contracts;
using MimeKit;
using Transformalize.Configuration;

namespace Transformalize.Providers.Mail {
    public class MailWriter : IWrite {
        private readonly OutputContext _context;
        private readonly Field[] _fields;
        private readonly bool _run = true;

        public MailWriter(OutputContext context) {
            _context = context;
            _fields = context.Entity.GetAllFields().Where(f => !f.System).ToArray();
            var provided = new HashSet<string>(_fields.Select(f => f.Alias.ToLower()).Distinct());
            var needed = new[] { "to", "from", "body" };

            foreach (var name in needed) {
                if (!provided.Contains(name)) {
                    _context.Warn($"Mail writer needs a {name} field in {_context.Entity.Alias}.");
                    _run = false;
                }
            }

        }

        public void Write(IEnumerable<IRow> rows) {

            if (!_run) {
                return;
            }

            // https://github.com/jstedfast/MailKit

            using (var client = new MailKit.Net.Smtp.SmtpClient()) {

                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(_context.Connection.Server, _context.Connection.Port, false);

                // Note: only needed if the SMTP server requires authentication
                if (_context.Connection.User != string.Empty) {
                    client.Authenticate(_context.Connection.User, _context.Connection.Password);
                }

                foreach (var row in rows) {
                    var message = new MimeMessage();

                    foreach (var field in _fields) {
                        switch (field.Alias.ToLower()) {
                            case "to":
                                message.To.Add(new MailboxAddress(row[field].ToString()));
                                break;
                            case "from":
                                message.From.Add(new MailboxAddress(row[field].ToString()));
                                break;
                            case "subject":
                                message.Subject = row[field].ToString();
                                break;
                            case "body":
                                if (field.Raw) {
                                    var builder = new BodyBuilder { HtmlBody = row[field].ToString() };
                                    message.Body = builder.ToMessageBody();
                                } else {
                                    message.Body = new TextPart("plain") { Text = row[field].ToString() };
                                }
                                break;
                        }
                    }

                    client.Send(message);
                }


                client.Disconnect(true);
            }
        }
    }
}