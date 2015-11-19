﻿(*** hide ***)
#load "AzureCluster.fsx"

open MBrace.Azure
open MBrace.Azure.Management

(**

# Provisioning your cluster using F# interactive

> If using a locally simulated cluster (via ThespianCluster.fsx) you can ignore this tutorial.

> This tutorial is from the [MBrace Starter Kit](https://github.com/mbraceproject/MBrace.StarterKit).

In this tutorial you learn how you can use the [MBrace.Azure.Management](https://www.nuget.org/packages/MBrace.Azure) library
to provision an MBrace cluster using F# Interactive. In order to proceed, you will need
to [sign up](https://azure.microsoft.com/en-us/pricing/free-trial/) for an Azure subscription. Once signed up, 
[download your publication settings file ](https://manage.windowsazure.com/publishsettings).

*)

// replace with your local .publishsettings path
let pubSettingsFile = @"C:\path\to\my.publishsettings" 

(**

Next, specify your region and the size and number of your virtual machines, and create your cluster:

*)
    
let region = Region.North_Europe
let vmSize = VMSize.Large
let vmCount = 4
    
let deployment = SubscriptionManager.Provision(pubSettingsFile, region, vmSize=vmSize, vmCount=vmCount) 

(**
This will provision a 4-worker MBrace cluster of Large (A3) instances
Provisioning can take some time (approximately 5 minutes). 
You can now connect to your cluster as follows:

*)

let cluster = AzureCluster.Connect(deployment.Configuration, logger = ConsoleLogger(true), logLevel = LogLevel.Info)

(**
However you must now also record your cluster details in AzureCluster.fsx. This lets you reconnect to your cluster later
and whenever you ``#load "AzureCluster.fsx"`` followed by ``let cluster = GetCluster()``:

    let pubSettingsFile = "..."
    let clusterName = "..."

Evaluating the following code snippet will insert you the values you need into AzureCluster.fsx:

*)

Config.RecordClusterDetails(pubSettingsFile, deployment)

(**
## Summary

In this tutorial, you've learned how to provision an MBrace clusters running on Azure using MBrace.Azure.Management.
You are now ready to proceed with further samples to learn more about the MBrace programming model.  

To learn more about provisioning, monitoring, scaling and deleting clusters using MBrace.Azure.Management
see [going-further/200-managing-azure-clusters.fsx](going-further/200-managing-azure-clusters.html).

*)


