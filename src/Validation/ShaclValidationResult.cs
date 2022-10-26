using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;

namespace IIB.ICDD.Validation
{
    public class ShaclValidationResult
    {
        public string Severity { get; set; }
        public string SourceShape { get; set; }
        public string FocusNode { get; set; }
        public string FocusPath { get; set; }
        public string ValidationMessage { get; set; }
        public string ResultValue { get; set; }

        public ShaclValidationResult(string severity, string sourceShape, string focusNode, string focusPath, string validationMessage, string resultValue)
        {
            Severity = severity;
            SourceShape = sourceShape;
            FocusNode = focusNode;
            ValidationMessage = validationMessage;
            ResultValue = resultValue;
            FocusPath = focusPath;
        }

        public static List<ShaclValidationResult> FromGraph(Graph graph)
        {
            var result = new List<ShaclValidationResult>();
            var predicateNode = graph.CreateUriNode(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#type"));
            var objectNode = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#ValidationResult"));
            var violations = graph.GetTriplesWithPredicateObject(predicateNode, objectNode);

            if (violations.Any())
            {
                foreach (var triple in violations)
                {
                    var subjectNode = triple.Subject;
                    var predicateNodeSeverity = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#resultSeverity"));
                    var predicateNodeFocus = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#focusNode"));
                    var predicateNodePath = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#resultPath"));
                    var predicateValuePath = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#value"));
                    var predicateNodeMessage = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#resultMessage"));
                    var predicateNodeSourceShape = graph.CreateUriNode(new Uri("http://www.w3.org/ns/shacl#sourceShape"));
                    var severityNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNodeSeverity);
                    var focusNodes = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNodeFocus);
                    var nodePaths = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNodePath);
                    var valuePaths = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateValuePath);
                    var nodeMessages = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNodeMessage);
                    var nodeSourceShape = graph.GetTriplesWithSubjectPredicate(subjectNode, predicateNodeSourceShape);
                    string severity, focus, path, message, value, sourceShape;
                    severity = focus = path = message = value = sourceShape = String.Empty;

                    if (severityNodes.Any())
                    {
                        severity = severityNodes.First()?.Object?.ToString();
                        severity = severity.Split('#').Last();
                    }

                    if (focusNodes.Any())
                    {
                        focus = focusNodes.First()?.Object?.ToString();

                    }

                    if (nodePaths.Any())
                    {
                        path = nodePaths.First()?.Object?.ToString();

                    }

                    if (nodeMessages.Any())
                    {
                        message = nodeMessages.First()?.Object?.ToString();
                    }

                    if (valuePaths.Any())
                    {
                        value = valuePaths.First()?.Object?.ToString();
                        if (value == "")
                        {
                            value = "[not defined value]";
                        }
                    }

                    if (nodeSourceShape.Any())
                    {
                        sourceShape = nodeSourceShape.First()?.Object?.ToString();

                    }
                    var res2 = new ShaclValidationResult(severity, sourceShape, focus, path, message, value);
                    result.Add(res2);

                }
            }
            else
            {
                var res = new ShaclValidationResult("Success", "all", "all", "all", "Sucess", "all valid");
                result.Add(res);
            }
            return result;
        }
    }
}
