This is a mail (output) provider for [Transformalize](https://github.com/dalenewman/Transformalize). It uses [MailKit](https://github.com/jstedfast/MailKit).
 
I've only tested messages using a local mail relay server and 
gmail SSL (smtp.gmail.com:465) with *allow less secure apps* on. 

```xml
<cfg name="Mail">

    <connections>
        <add name="input" provider="internal" />
        <add name="output" provider="mail" server="mail.dail.local" port="25" />
    </connections>

    <entities>
        <add name="Messages" >
            <rows>
                <add From="dale@dale.com" To="dale@dale.com" Cc="" Bcc="" Subject="Test" Body="I am a test message." />
            </rows>
            <fields>
                <add name="From" />
                <add name="To" />
                <add name="Cc" />
                <add name="Ncc" />
                <add name="Subject" />
                <add name="Body" />
            </fields>
        </add>
    </entities>

</cfg>
```

Saving this as *email.xml* and running should send the message.  When using a mail 
output, your entity must have `from`, `to`, and `body` fields.

This provider will be available as a plugin for Transformalize 0.2.11-beta.