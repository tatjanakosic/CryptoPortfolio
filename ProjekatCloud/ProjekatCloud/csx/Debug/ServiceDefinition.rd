<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ProjekatCloud" generation="1" functional="0" release="0" Id="8b57ea33-b716-42bd-8372-120244376518" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ProjekatCloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HealthMonitoringService:healthmonitoring" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/LB:HealthMonitoringService:healthmonitoring" />
          </inToChannel>
        </inPort>
        <inPort name="HealthStatusService:healthstatus" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/LB:HealthStatusService:healthstatus" />
          </inToChannel>
        </inPort>
        <inPort name="PortfolioService:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/LB:PortfolioService:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HealthMonitoringService:LogConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapHealthMonitoringService:LogConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthMonitoringService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapHealthMonitoringService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthMonitoringServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapHealthMonitoringServiceInstances" />
          </maps>
        </aCS>
        <aCS name="HealthStatusService:LogConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapHealthStatusService:LogConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthStatusService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapHealthStatusService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthStatusServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapHealthStatusServiceInstances" />
          </maps>
        </aCS>
        <aCS name="NotificationService:AlarmConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapNotificationService:AlarmConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationService:AlarmDoneConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapNotificationService:AlarmDoneConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationService:CryptoConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapNotificationService:CryptoConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapNotificationService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationService:QueueConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapNotificationService:QueueConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapNotificationServiceInstances" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:AlarmConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:AlarmConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:AlarmDoneConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:AlarmDoneConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:CryptoConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:CryptoConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:DataConnectionStringBlob" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:DataConnectionStringBlob" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:LogConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:LogConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioService:QueueConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioService:QueueConnectionString" />
          </maps>
        </aCS>
        <aCS name="PortfolioServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ProjekatCloud/ProjekatCloudGroup/MapPortfolioServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HealthMonitoringService:healthmonitoring">
          <toPorts>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringService/healthmonitoring" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:HealthStatusService:healthstatus">
          <toPorts>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusService/healthstatus" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:PortfolioService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/Endpoint1" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:NotificationService:Input_WorkerRole">
          <toPorts>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationService/Input_WorkerRole" />
          </toPorts>
        </sFSwitchChannel>
        <sFSwitchChannel name="SW:PortfolioService:Input_WebRole">
          <toPorts>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/Input_WebRole" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapHealthMonitoringService:LogConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringService/LogConnectionString" />
          </setting>
        </map>
        <map name="MapHealthMonitoringService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHealthMonitoringServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringServiceInstances" />
          </setting>
        </map>
        <map name="MapHealthStatusService:LogConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusService/LogConnectionString" />
          </setting>
        </map>
        <map name="MapHealthStatusService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHealthStatusServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusServiceInstances" />
          </setting>
        </map>
        <map name="MapNotificationService:AlarmConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationService/AlarmConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationService:AlarmDoneConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationService/AlarmDoneConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationService:CryptoConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationService/CryptoConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationService:QueueConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationService/QueueConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationServiceInstances" />
          </setting>
        </map>
        <map name="MapPortfolioService:AlarmConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/AlarmConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioService:AlarmDoneConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/AlarmDoneConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioService:CryptoConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/CryptoConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioService:DataConnectionStringBlob" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/DataConnectionStringBlob" />
          </setting>
        </map>
        <map name="MapPortfolioService:LogConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/LogConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioService:QueueConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService/QueueConnectionString" />
          </setting>
        </map>
        <map name="MapPortfolioServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HealthMonitoringService" generation="1" functional="0" release="0" software="C:\Users\korisnik\Desktop\projekatcloud2\ProjekatCloud\ProjekatCloud\ProjekatCloud\csx\Debug\roles\HealthMonitoringService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="healthmonitoring" protocol="tcp" portRanges="10100" />
              <outPort name="NotificationService:Input_WorkerRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:NotificationService:Input_WorkerRole" />
                </outToChannel>
              </outPort>
              <outPort name="PortfolioService:Input_WebRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:PortfolioService:Input_WebRole" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="LogConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthMonitoringService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;healthmonitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;healthstatus&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;Input_WorkerRole&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;PortfolioService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Input_WebRole&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="HealthStatusService" generation="1" functional="0" release="0" software="C:\Users\korisnik\Desktop\projekatcloud2\ProjekatCloud\ProjekatCloud\ProjekatCloud\csx\Debug\roles\HealthStatusService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="healthstatus" protocol="http" portRanges="8080" />
              <outPort name="NotificationService:Input_WorkerRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:NotificationService:Input_WorkerRole" />
                </outToChannel>
              </outPort>
              <outPort name="PortfolioService:Input_WebRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:PortfolioService:Input_WebRole" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="LogConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthStatusService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;healthmonitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;healthstatus&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;Input_WorkerRole&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;PortfolioService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Input_WebRole&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="NotificationService" generation="1" functional="0" release="0" software="C:\Users\korisnik\Desktop\projekatcloud2\ProjekatCloud\ProjekatCloud\ProjekatCloud\csx\Debug\roles\NotificationService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Input_WorkerRole" protocol="tcp" portRanges="10101" />
              <outPort name="NotificationService:Input_WorkerRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:NotificationService:Input_WorkerRole" />
                </outToChannel>
              </outPort>
              <outPort name="PortfolioService:Input_WebRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:PortfolioService:Input_WebRole" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="AlarmConnectionString" defaultValue="" />
              <aCS name="AlarmDoneConnectionString" defaultValue="" />
              <aCS name="CryptoConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="QueueConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;NotificationService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;healthmonitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;healthstatus&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;Input_WorkerRole&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;PortfolioService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Input_WebRole&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/NotificationServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="PortfolioService" generation="1" functional="0" release="0" software="C:\Users\korisnik\Desktop\projekatcloud2\ProjekatCloud\ProjekatCloud\ProjekatCloud\csx\Debug\roles\PortfolioService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Input_WebRole" protocol="tcp" portRanges="8081" />
              <outPort name="NotificationService:Input_WorkerRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:NotificationService:Input_WorkerRole" />
                </outToChannel>
              </outPort>
              <outPort name="PortfolioService:Input_WebRole" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ProjekatCloud/ProjekatCloudGroup/SW:PortfolioService:Input_WebRole" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="AlarmConnectionString" defaultValue="" />
              <aCS name="AlarmDoneConnectionString" defaultValue="" />
              <aCS name="CryptoConnectionString" defaultValue="" />
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="DataConnectionStringBlob" defaultValue="" />
              <aCS name="LogConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="QueueConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;PortfolioService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;healthmonitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;healthstatus&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;Input_WorkerRole&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;PortfolioService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Input_WebRole&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="PortfolioServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthStatusServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="NotificationServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthMonitoringServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="HealthMonitoringServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="HealthStatusServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="NotificationServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="PortfolioServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HealthMonitoringServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="HealthStatusServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="NotificationServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="PortfolioServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="3f52c3d6-bc2e-4a08-8dc0-c84cbb298fbc" ref="Microsoft.RedDog.Contract\ServiceContract\ProjekatCloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="19d12daf-4767-49c1-a145-c11c651ee665" ref="Microsoft.RedDog.Contract\Interface\HealthMonitoringService:healthmonitoring@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthMonitoringService:healthmonitoring" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="0fa57dfa-e780-479c-8af7-df9da8d57de2" ref="Microsoft.RedDog.Contract\Interface\HealthStatusService:healthstatus@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/HealthStatusService:healthstatus" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="1d5f135a-4819-4f48-bf24-61ef1c35481b" ref="Microsoft.RedDog.Contract\Interface\PortfolioService:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ProjekatCloud/ProjekatCloudGroup/PortfolioService:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>