# IIB.ICDD framework [![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.7256174.svg)](https://doi.org/10.5281/zenodo.7256174) 
The IIB.ICDD framework provides functions in C# [.NET 6](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6) to open, create, validate, edit, export containers defined by [ISO 21597-1:2020](https://www.iso.org/standard/74389.html). In ISO 21597-1, a specification is given for a generic container format that stores documents using various formats and structures, along with a means of linking otherwise disconnected data within those documents (including individual parts). These documents can have any syntax and semantics. The container format includes a header file and optional link files that define relationships by including references to the documents, or to elements within them. The header file uniquely identifies the container and its contractual or collaborative intention. This information is defined using the [RDF](https://www.w3.org/TR/rdf11-concepts/), [RDFS](https://www.w3.org/TR/rdf-schema/), and [OWL](https://www.w3.org/TR/owl2-overview/) semantic web standards. Querying these containers with [SPARQL](https://www.w3.org/TR/sparql11-query/) as well as using [SHACL](https://www.w3.org/TR/shacl) for validation is provided by this framework.

## How-to use this code
* see [INSTRUCTIONS](https://philhag.github.io/IIB.ICDD/) file

## Download
* [IIB.ICDD releases](https://github.com/philhag/IIB.ICDD/releases)
* [NuGet Package](https://www.nuget.org/packages/ICDDToolkitCore/)

## License 
* see [LICENSE](https://github.com/philhag/IIB.ICDD/blob/main/LICENSE) file

### Third party libraries
* [dotNetRDF (>= 2.7.5)](https://www.nuget.org/packages/dotNetRDF/)
* [r2rml4net (>= 0.8.0)](https://www.nuget.org/packages/r2rml4net/)
* [System.Data.SqlClient (>= 4.8.3)](https://www.nuget.org/packages/System.Data.SqlClient/)
* [LibGit2Sharp (>= 0.27.0)](https://www.nuget.org/packages/LibGit2Sharp/)


## Contact
#### Developer
* Homepage: [https://www.inf.bi.ruhr-uni-bochum.de/](https://www.inf.bi.ruhr-uni-bochum.de/)
* e-mail: philipp.hagedorn-n6v@ruhr-uni-bochum.de