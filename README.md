# foundry-infra-cs

This repo uses [Terraform CDK][terraform-cdk] to generate DigitalOcean resources for Foundry servers. 

[terraform-cdk]: https://github.com/hashicorp/terraform-cdk

# Purpose

This repository contains a CLI project that can be executed to generate Terraform files for use by the cdk-cli. The cdk-cli can deploy these files as real cloud infrastructure. This project will be a re-write of [foundry-infra Terragrunt project][foundry-infra] with a focus on improving modularity and simplicity.

# Pre-Requisites

- dotnet cli with dotnet 5
- node and npm

# Setup

Run `npm install` to install the cdktf tools.

Run `npx cdktf get` to install the providers. Will create the `.gen` directory.

Now run the build commands in your editor, e.g. `dotnet run`. 

Or run the diff deploy commands with `npx cdktf diff` or `npx cdktf deploy`.

## Secrets and Configuration

The non-sensitive configuration is under the `"Config"` key in `appsettings.json`. Some data is sensitive, and must be made available in some other form e.g. dotnet user-secrets. The required variables are:

- `Config:Provider:DigitalOceanToken`
- `Config:Provider:DigitalOceanSpacesSecretKey`
- `Config:Provider:DigitalOceanSpacesAccessId`

### `dotnet` Development Secrets

To store secrets with `dotnet user-secrets`:

1. `dotnet user-secrets init`
2. `dotnet user-secrets set "Config:Provider:DigitalOceanToken" "fjsdlkfjlksdfjlksdjflksdj..."`

After secrets are set, be sure to run the application with the environment variable: `DOTNET_ENVIRONMENT=Development`.

## cdktf cli

If you already have `cdktf-cli` installed globally, you may omit the `npx` parts of each `cdktf` command.

## Notes for Windows Users

Until [this issue][cdktf-get-bug] is resolved, run `cdktf get` with admin privileges. 

# Internals

This repository holds a single CLI project. It uses [Generic Host][generic-host] to setup logging, service injection, and configuration. A single ConsoleHostedService is spun off that executes the CDK synthesis operations and then shuts-down the generic host.

[cdktf-get-bug]: https://github.com/hashicorp/terraform-cdk/issues/501
[foundry-infra]: https://github.com/foundry-infra/foundry-infra
[generic-host]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host
