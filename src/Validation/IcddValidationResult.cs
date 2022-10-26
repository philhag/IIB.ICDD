using System;

namespace IIB.ICDD.Validation
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  IcddValidationResult 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class IcddValidationResult
    {
        public static bool ValidBoolResult = true;
        public string ValidationCriterion { get; set; }
        public object ExaminedValue { get; set; }
        public object ExpectedValue { get; set; }
        public ValidationGroup ValidationGroup { get; set; }
        public ValidationType ValidationType { get; set; }

        public IcddValidationResult(string name, object expVal, ValidationGroup valGrp)
        {
            ValidationCriterion = name;
            ExpectedValue = expVal;
            ValidationGroup = valGrp;

        }

        

        public bool ValidationResult()
        {
            return ExaminedValue.Equals(ExpectedValue);
        }

        public new string ToString()
        {
            var dt = DateTime.Now.ToLongTimeString();
            return ("[Type="+ ValidationType + ", Group="+ ValidationGroup+ ", Date=" + dt + "]: " + ValidationCriterion + " , Passed:" + ValidationResult());
        }
        public string GetGroupName()
        {
            switch (ValidationGroup)
            {
                case ValidationGroup.Part1Container:
                {
                    return "Part 1: Container";
                }
                case ValidationGroup.Part1Index:
                {
                    return "Part 1: Header File";
                }
                case ValidationGroup.Part1Linkset:
                {
                    return "Part 1: Linksets";
                }
                case ValidationGroup.Part1Documents:
                {
                    return "Part 1: Documents";
                }
                case ValidationGroup.Part2:
                {
                    return "Part 2: ...";
                }

            }
            return "ICCD";
        }
    }

    public enum ValidationGroup
    {
        Part1Container = 1,
        Part1Index = 2,
        Part1Linkset = 3,
        Part1Documents = 4,
        Part2 = 5,
        Shacl =6
    }

    public enum ValidationType
    {
        Conformity = 1,
        GraphLogic = 2,
        Shacl = 3
    }
}