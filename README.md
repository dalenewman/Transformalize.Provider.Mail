This is a mail (output) provider for [Transformalize](https://github.com/dalenewman/Transformalize).  
It uses [MailKit](https://github.com/jstedfast/MailKit).
 
I've only tested text messages using a mail relay server (which doesn't require authentication). Eventually, 
different types of authentication will be implemented, and maybe even an input provider as well.

```xml
<cfg name="Mail">

    <connections>
        <add name="input" provider="internal" />
        <add name="output" provider="mail" server="mail.dail.local" port="25" />
    </connections>

    <entities>
        <rows>
            <add From="dale@dale.com" To="dale@dale.com" Subject="Test" Body="I am a test message." />
        </rows>
        <add name="Messages" >
            <fields>
                <add name="From" />
                <add name="To" />
                <add name="Subject" />
                <add name="Body" />
            </fields>
        </add>
    </entities>

</cfg>
```

Saving this as *email.xml* and running should send the message.  When using a mail 
output, your input must have `from`, `to`, and `body` fields in your entity.

This provider will be available as a plugin for Transformalize 0.2.9-beta.