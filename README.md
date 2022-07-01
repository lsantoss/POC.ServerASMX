# POC.ServerASMX

## Application:
This application contains an example of ASMX Server

> Unable to perform dependency injection

> Cannot use interfaces as return type

> Return classes must contain an empty constructor (for self-realization)

---

## Frameworks:
- .Net Framework 4.7.2

---
## Libraries (only most important):
- Dapper
- Microsoft.CodeDom.Providers.DotNetCompilerPlatform
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets
- Newtonsoft.Json
- NUnit
- NUnit.Analyzers
- NUnit3TestAdapter

## How to configure and use:
- Create an Empty Asp.Net Web Project (.Net Framework)
- Add a new Web Service file (ASMX)
- Nos métodos use a anotação:  `[WebMethod]`
- Method example:
  ```c#
  [WebMethod]
  public List<CustomerQueryResult> List() => _repository.List();
  ```