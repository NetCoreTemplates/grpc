# grpc

gRPC .NET Core 3.1 Web Template

> Browse [source code](https://github.com/NetCoreTemplates/grpc) and install with [x new dotnet tool](https://docs.servicestack.net/web-new):

    $ dotnet tool install -g x

    $ x new grpc ProjectName

Please refer to gRPC docs to learn more about ServiceStack gRPC and SSL Configuration:

 - http://docs.servicestack.net/grpc
 - http://docs.servicestack.net/grpc-ssl

By default this uses ASP.NET Core's trusted Development certificate (typically created on install), or can be configured with:

    $ dotnet dev-certs https --trust

This template also includes OpenSSL generation scripts in [scripts/](https://github.com/NetCoreTemplates/grpc/tree/master/scripts)
should you prefer to use your own self-signed certificates:

    $ cd scripts

### Generating a new Development Certificate

Windows:

    C:\> bash gen-dev.https.sh

Linux or WSL Bash:

    $ ./gen-dev.https.sh

Options:

    $ gen-dev.https.sh <PASSWORD>

Import the pfx certificate:

    $ powershell Import-PfxCertificate -FilePath dev.pfx Cert:\LocalMachine\My -Password (ConvertTo-SecureString grpc -asplaintext -force) -Exportable

Trust the certificate by importing the pfx certificate into your trusted root:

    $ powershell Import-Certificate -FilePath dev.crt -CertStoreLocation Cert:\CurrentUser\Root

### Generating a new Production Certificate

Either replace `DOMAIN=...` and `PASSWORD=...` with your domain and password or specify them as arguments, e.g:

Windows:

    C:\> bash gen-prod.https.sh <DOMAIN> <PASSWORD>

Linux or WSL Bash:

    $ ./gen-prod.https.sh <DOMAIN> <PASSWORD>
