using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transformalize.Configuration;
using Transformalize.Containers.Autofac;
using Transformalize.Contracts;
using Transformalize.Providers.Console;
using Transformalize.Providers.Mail.Autofac;

namespace IntegrationTests {
   [TestClass]
   public class UnitTest1 {

      [Ignore("it's a pain to test this")]
      [TestMethod]
      public void TestWrite() {
         const string xml = @"<cfg name='Mail'>

    <connections>
        <add name='input' provider='internal' />
        <add name='output' provider='mail' server='*' port='*' />
    </connections>

    <entities>
        <add name='Messages'>
            <rows>
                <add From='*' To='*' Cc='' Bcc='' Subject='Test' Body='I am a test message.' />
            </rows>
            <fields>
                <add name='From' />
                <add name='To' />
                <add name='Cc' />
                <add name='Ncc' />
                <add name='Subject' />
                <add name='Body' />
            </fields>
        </add>
    </entities>

</cfg>";

         var logger = new ConsoleLogger(LogLevel.Debug);
         using (var outer = new ConfigurationContainer().CreateScope(xml, logger)) {
            var process = outer.Resolve<Process>();
            using (var inner = new Container(new MailModule()).CreateScope(process, logger)) {
               var controller = inner.Resolve<IProcessController>();
               controller.Execute();
            }
         }
      }
   }
}
