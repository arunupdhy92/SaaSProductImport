# SaaSProductImport
Import product from different sources
**Coding Guide:**
Prerequisite:
1. It's created on .Net core 3.1 framework.
2. it require below packages for work:
    I. Microsoft.AspNetCore.Mvc.Core (Version: 2.2.5 or above)
    II. Newtonsoft.Json (Version: 13.0.1 or above)
    III. YamlDotNet (Version: 11.2.1 or above)
    
 Installation Steps:
 1. Clone this Repo in your local system.
 2. Open it in Visual Studio and run it.
 3. When it ask for your input. enter any of the below command:
    import capterra feed-products/capterra.yaml
    import softwareadvice feed-product/softwareadvice.json
    
 **DB Assginement:**
 You can find all required SQL script inside your cloned directory at below path:
    SaaSProductImport\SqlScript\DatabaseAssignment.sql
 Note: These queries are written in SQL Server.
