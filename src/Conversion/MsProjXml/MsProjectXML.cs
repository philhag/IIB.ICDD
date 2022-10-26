using System;
using System.Xml.Serialization;

namespace IIB.ICDD.Conversion.MsProjXml
{
    /// <summary>
    /// Icdd Toolkit Library
    /// Class:  MsProjectXML 
    /// (c) 2022 Philipp Hagedorn, Chair of Computing in Engineering, Ruhr-University Bochum, Germany
    /// Mail: philipp.hagedorn-n6v@rub.de 
    /// </summary>
    public class MsProjectXML
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        [XmlRoot(Namespace = "http://schemas.microsoft.com/project", IsNullable = true)]
        public partial class Project
        {

            private int saveVersionField;

            private string uIDField;

            private string nameField;

            private string titleField;

            private string subjectField;

            private string categoryField;

            private string companyField;

            private string managerField;

            private string authorField;

            private DateTime? creationDateField;

            private int revisionField;

            private DateTime? lastSavedField;

            private bool scheduleFromStartField;

            private DateTime? startDateField;

            private DateTime? finishDateField;

            private int fYStartDateField;

            private int criticalSlackLimitField;

            private int currencyDigitsField;

            private string currencySymbolField;

            private string currencyCodeField;

            private int currencySymbolPositionField;

            private int calendarUIDField;

            private DateTime? defaultStartTimeField;

            private DateTime? defaultFinishTimeField;

            private int minutesPerDayField;

            private int minutesPerWeekField;

            private int daysPerMonthField;

            private int defaultTaskTypeField;

            private int defaultFixedCostAccrualField;

            private int defaultStandardRateField;

            private int defaultOvertimeRateField;

            private int durationFormatField;

            private int workFormatField;

            private bool editableActualCostsField;

            private bool honorConstraintsField;

            private int earnedValueMethodField;

            private bool insertedProjectsLikeSummaryField;

            private bool multipleCriticalPathsField;

            private bool newTasksEffortDrivenField;

            private bool newTasksEstimatedField;

            private bool splitsInProgressTasksField;

            private bool spreadActualCostField;

            private bool spreadPercentCompleteField;

            private bool taskUpdatesResourceField;

            private bool fiscalYearStartField;

            private int weekStartDayField;

            private bool moveCompletedEndsBackField;

            private bool moveRemainingStartsBackField;

            private bool moveRemainingStartsForwardField;

            private bool moveCompletedEndsForwardField;

            private int baselineForEarnedValueField;

            private bool autoAddNewResourcesAndTasksField;

            private DateTime? statusDateField;

            private DateTime? currentDateField;

            private bool microsoftProjectServerURLField;

            private bool autolinkField;

            private int newTaskStartDateField;

            private int defaultTaskEVMethodField;

            private bool projectExternallyEditedField;

            private DateTime? extendedCreationDateField;

            private bool actualsInSyncField;

            private bool removeFilePropertiesField;

            private bool adminProjectField;

            private ProjectOutlineCode[] outlineCodesField;

            private ProjectWBSMasks wBSMasksField;

            private ProjectExtendedAttribute[] extendedAttributesField;

            private ProjectCalendar[] calendarsField;

            private ProjectTask[] tasksField;

            private ProjectResource[] resourcesField;

            private ProjectAssignment[] assignmentsField;

            /// <remarks/>
            public int SaveVersion
            {
                get
                {
                    return saveVersionField;
                }
                set
                {
                    saveVersionField = value;
                }
            }

            /// <remarks/>
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            public string Title
            {
                get
                {
                    return titleField;
                }
                set
                {
                    titleField = value;
                }
            }

            /// <remarks/>
            public string Subject
            {
                get
                {
                    return subjectField;
                }
                set
                {
                    subjectField = value;
                }
            }

            /// <remarks/>
            public string Category
            {
                get
                {
                    return categoryField;
                }
                set
                {
                    categoryField = value;
                }
            }

            /// <remarks/>
            public string Company
            {
                get
                {
                    return companyField;
                }
                set
                {
                    companyField = value;
                }
            }

            /// <remarks/>
            public string Manager
            {
                get
                {
                    return managerField;
                }
                set
                {
                    managerField = value;
                }
            }

            /// <remarks/>
            public string Author
            {
                get
                {
                    return authorField;
                }
                set
                {
                    authorField = value;
                }
            }

            /// <remarks/>
            public DateTime? CreationDate
            {
                get
                {
                    return creationDateField;
                }
                set
                {
                    creationDateField = value;
                }
            }

            /// <remarks/>
            public int Revision
            {
                get
                {
                    return revisionField;
                }
                set
                {
                    revisionField = value;
                }
            }

            /// <remarks/>
            public DateTime? LastSaved
            {
                get
                {
                    return lastSavedField;
                }
                set
                {
                    lastSavedField = value;
                }
            }

            /// <remarks/>
            public bool ScheduleFromStart
            {
                get
                {
                    return scheduleFromStartField;
                }
                set
                {
                    scheduleFromStartField = value;
                }
            }

            /// <remarks/>
            public DateTime? StartDate
            {
                get
                {
                    return startDateField;
                }
                set
                {
                    startDateField = value;
                }
            }

            /// <remarks/>
            public DateTime? FinishDate
            {
                get
                {
                    return finishDateField;
                }
                set
                {
                    finishDateField = value;
                }
            }

            /// <remarks/>
            public int FYStartDate
            {
                get
                {
                    return fYStartDateField;
                }
                set
                {
                    fYStartDateField = value;
                }
            }

            /// <remarks/>
            public int CriticalSlackLimit
            {
                get
                {
                    return criticalSlackLimitField;
                }
                set
                {
                    criticalSlackLimitField = value;
                }
            }

            /// <remarks/>
            public int CurrencyDigits
            {
                get
                {
                    return currencyDigitsField;
                }
                set
                {
                    currencyDigitsField = value;
                }
            }

            /// <remarks/>
            public string CurrencySymbol
            {
                get
                {
                    return currencySymbolField;
                }
                set
                {
                    currencySymbolField = value;
                }
            }

            /// <remarks/>
            public string CurrencyCode
            {
                get
                {
                    return currencyCodeField;
                }
                set
                {
                    currencyCodeField = value;
                }
            }

            /// <remarks/>
            public int CurrencySymbolPosition
            {
                get
                {
                    return currencySymbolPositionField;
                }
                set
                {
                    currencySymbolPositionField = value;
                }
            }

            /// <remarks/>
            public int CalendarUID
            {
                get
                {
                    return calendarUIDField;
                }
                set
                {
                    calendarUIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "time")]
            [System.ComponentModel.DefaultValueAttribute(typeof(DateTime), "1901-01-01")]
            [XmlIgnore]
            public DateTime? DefaultStartTime
            {
                get
                {
                    return defaultStartTimeField;
                }
                set
                {
                    defaultStartTimeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "time")]
            [System.ComponentModel.DefaultValueAttribute(typeof(DateTime), "1901-01-01")]
            [XmlIgnore]
            public DateTime? DefaultFinishTime
            {
                get
                {
                    return defaultFinishTimeField;
                }
                set
                {
                    defaultFinishTimeField = value;
                }
            }

            /// <remarks/>
            public int MinutesPerDay
            {
                get
                {
                    return minutesPerDayField;
                }
                set
                {
                    minutesPerDayField = value;
                }
            }

            /// <remarks/>
            public int MinutesPerWeek
            {
                get
                {
                    return minutesPerWeekField;
                }
                set
                {
                    minutesPerWeekField = value;
                }
            }

            /// <remarks/>
            public int DaysPerMonth
            {
                get
                {
                    return daysPerMonthField;
                }
                set
                {
                    daysPerMonthField = value;
                }
            }

            /// <remarks/>
            public int DefaultTaskType
            {
                get
                {
                    return defaultTaskTypeField;
                }
                set
                {
                    defaultTaskTypeField = value;
                }
            }

            /// <remarks/>
            public int DefaultFixedCostAccrual
            {
                get
                {
                    return defaultFixedCostAccrualField;
                }
                set
                {
                    defaultFixedCostAccrualField = value;
                }
            }

            /// <remarks/>
            public int DefaultStandardRate
            {
                get
                {
                    return defaultStandardRateField;
                }
                set
                {
                    defaultStandardRateField = value;
                }
            }

            /// <remarks/>
            public int DefaultOvertimeRate
            {
                get
                {
                    return defaultOvertimeRateField;
                }
                set
                {
                    defaultOvertimeRateField = value;
                }
            }

            /// <remarks/>
            public int DurationFormat
            {
                get
                {
                    return durationFormatField;
                }
                set
                {
                    durationFormatField = value;
                }
            }

            /// <remarks/>
            public int WorkFormat
            {
                get
                {
                    return workFormatField;
                }
                set
                {
                    workFormatField = value;
                }
            }

            /// <remarks/>
            public bool EditableActualCosts
            {
                get
                {
                    return editableActualCostsField;
                }
                set
                {
                    editableActualCostsField = value;
                }
            }

            /// <remarks/>
            public bool HonorConstraints
            {
                get
                {
                    return honorConstraintsField;
                }
                set
                {
                    honorConstraintsField = value;
                }
            }

            /// <remarks/>
            public int EarnedValueMethod
            {
                get
                {
                    return earnedValueMethodField;
                }
                set
                {
                    earnedValueMethodField = value;
                }
            }

            /// <remarks/>
            public bool InsertedProjectsLikeSummary
            {
                get
                {
                    return insertedProjectsLikeSummaryField;
                }
                set
                {
                    insertedProjectsLikeSummaryField = value;
                }
            }

            /// <remarks/>
            public bool MultipleCriticalPaths
            {
                get
                {
                    return multipleCriticalPathsField;
                }
                set
                {
                    multipleCriticalPathsField = value;
                }
            }

            /// <remarks/>
            public bool NewTasksEffortDriven
            {
                get
                {
                    return newTasksEffortDrivenField;
                }
                set
                {
                    newTasksEffortDrivenField = value;
                }
            }

            /// <remarks/>
            public bool NewTasksEstimated
            {
                get
                {
                    return newTasksEstimatedField;
                }
                set
                {
                    newTasksEstimatedField = value;
                }
            }

            /// <remarks/>
            public bool SplitsInProgressTasks
            {
                get
                {
                    return splitsInProgressTasksField;
                }
                set
                {
                    splitsInProgressTasksField = value;
                }
            }

            /// <remarks/>
            public bool SpreadActualCost
            {
                get
                {
                    return spreadActualCostField;
                }
                set
                {
                    spreadActualCostField = value;
                }
            }

            /// <remarks/>
            public bool SpreadPercentComplete
            {
                get
                {
                    return spreadPercentCompleteField;
                }
                set
                {
                    spreadPercentCompleteField = value;
                }
            }

            /// <remarks/>
            public bool TaskUpdatesResource
            {
                get
                {
                    return taskUpdatesResourceField;
                }
                set
                {
                    taskUpdatesResourceField = value;
                }
            }

            /// <remarks/>
            public bool FiscalYearStart
            {
                get
                {
                    return fiscalYearStartField;
                }
                set
                {
                    fiscalYearStartField = value;
                }
            }

            /// <remarks/>
            public int WeekStartDay
            {
                get
                {
                    return weekStartDayField;
                }
                set
                {
                    weekStartDayField = value;
                }
            }

            /// <remarks/>
            public bool MoveCompletedEndsBack
            {
                get
                {
                    return moveCompletedEndsBackField;
                }
                set
                {
                    moveCompletedEndsBackField = value;
                }
            }

            /// <remarks/>
            public bool MoveRemainingStartsBack
            {
                get
                {
                    return moveRemainingStartsBackField;
                }
                set
                {
                    moveRemainingStartsBackField = value;
                }
            }

            /// <remarks/>
            public bool MoveRemainingStartsForward
            {
                get
                {
                    return moveRemainingStartsForwardField;
                }
                set
                {
                    moveRemainingStartsForwardField = value;
                }
            }

            /// <remarks/>
            public bool MoveCompletedEndsForward
            {
                get
                {
                    return moveCompletedEndsForwardField;
                }
                set
                {
                    moveCompletedEndsForwardField = value;
                }
            }

            /// <remarks/>
            public int BaselineForEarnedValue
            {
                get
                {
                    return baselineForEarnedValueField;
                }
                set
                {
                    baselineForEarnedValueField = value;
                }
            }

            /// <remarks/>
            public bool AutoAddNewResourcesAndTasks
            {
                get
                {
                    return autoAddNewResourcesAndTasksField;
                }
                set
                {
                    autoAddNewResourcesAndTasksField = value;
                }
            }

            /// <remarks/>
            public DateTime? StatusDate
            {
                get
                {
                    return statusDateField;
                }
                set
                {
                    statusDateField = value;
                }
            }

            /// <remarks/>
            public DateTime? CurrentDate
            {
                get
                {
                    return currentDateField;
                }
                set
                {
                    currentDateField = value;
                }
            }

            /// <remarks/>
            public bool MicrosoftProjectServerURL
            {
                get
                {
                    return microsoftProjectServerURLField;
                }
                set
                {
                    microsoftProjectServerURLField = value;
                }
            }

            /// <remarks/>
            public bool Autolink
            {
                get
                {
                    return autolinkField;
                }
                set
                {
                    autolinkField = value;
                }
            }

            /// <remarks/>
            public int NewTaskStartDate
            {
                get
                {
                    return newTaskStartDateField;
                }
                set
                {
                    newTaskStartDateField = value;
                }
            }

            /// <remarks/>
            public int DefaultTaskEVMethod
            {
                get
                {
                    return defaultTaskEVMethodField;
                }
                set
                {
                    defaultTaskEVMethodField = value;
                }
            }

            /// <remarks/>
            public bool ProjectExternallyEdited
            {
                get
                {
                    return projectExternallyEditedField;
                }
                set
                {
                    projectExternallyEditedField = value;
                }
            }

            /// <remarks/>
            [XmlIgnore]
            public DateTime? ExtendedCreationDate
            {
                get
                {
                    return extendedCreationDateField;
                }
                set
                {
                    extendedCreationDateField = value;
                }
            }

            /// <remarks/>
            public bool ActualsInSync
            {
                get
                {
                    return actualsInSyncField;
                }
                set
                {
                    actualsInSyncField = value;
                }
            }

            /// <remarks/>
            public bool RemoveFileProperties
            {
                get
                {
                    return removeFilePropertiesField;
                }
                set
                {
                    removeFilePropertiesField = value;
                }
            }

            /// <remarks/>
            public bool AdminProject
            {
                get
                {
                    return adminProjectField;
                }
                set
                {
                    adminProjectField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("OutlineCode", IsNullable = true)]
            [XmlIgnore]
            public ProjectOutlineCode[] OutlineCodes
            {
                get
                {
                    return outlineCodesField;
                }
                set
                {
                    outlineCodesField = value;
                }
            }

            /// <remarks/>
            [XmlIgnore]
            public ProjectWBSMasks WBSMasks
            {
                get
                {
                    return wBSMasksField;
                }
                set
                {
                    wBSMasksField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("ExtendedAttribute", IsNullable = true)]
            [XmlIgnore]
            public ProjectExtendedAttribute[] ExtendedAttributes
            {
                get
                {
                    return extendedAttributesField;
                }
                set
                {
                    extendedAttributesField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Calendar", IsNullable = true)]
            [XmlIgnore]
            public ProjectCalendar[] Calendars
            {
                get
                {
                    return calendarsField;
                }
                set
                {
                    calendarsField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Task", IsNullable = true)]
            
            public ProjectTask[] Tasks
            {
                get
                {
                    return tasksField;
                }
                set
                {
                    tasksField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Resource", IsNullable = true)]
            [XmlIgnore]
            public ProjectResource[] Resources
            {
                get
                {
                    return resourcesField;
                }
                set
                {
                    resourcesField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Assignment", IsNullable = true)]
            [XmlIgnore]
            public ProjectAssignment[] Assignments
            {
                get
                {
                    return assignmentsField;
                }
                set
                {
                    assignmentsField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectOutlineCode
        {

            private string guidField;

            private string fieldIDField;

            private string fieldNameField;

            private string aliasField;

            private string phoneticAliasField;

            private ProjectOutlineCodeValue[] valuesField;

            private bool enterpriseField;

            private string enterpriseOutlineCodeAliasField;

            private bool resourceSubstitutionEnabledField;

            private bool leafOnlyField;

            private bool allLevelsRequiredField;

            private bool onlyTableValuesAllowedField;

            private ProjectOutlineCodeMask[] masksField;

            /// <remarks/>
            public string Guid
            {
                get
                {
                    return guidField;
                }
                set
                {
                    guidField = value;
                }
            }

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            public string FieldName
            {
                get
                {
                    return fieldNameField;
                }
                set
                {
                    fieldNameField = value;
                }
            }

            /// <remarks/>
            public string Alias
            {
                get
                {
                    return aliasField;
                }
                set
                {
                    aliasField = value;
                }
            }

            /// <remarks/>
            public string PhoneticAlias
            {
                get
                {
                    return phoneticAliasField;
                }
                set
                {
                    phoneticAliasField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Value", IsNullable = false)]
            public ProjectOutlineCodeValue[] Values
            {
                get
                {
                    return valuesField;
                }
                set
                {
                    valuesField = value;
                }
            }

            /// <remarks/>
            public bool Enterprise
            {
                get
                {
                    return enterpriseField;
                }
                set
                {
                    enterpriseField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string EnterpriseOutlineCodeAlias
            {
                get
                {
                    return enterpriseOutlineCodeAliasField;
                }
                set
                {
                    enterpriseOutlineCodeAliasField = value;
                }
            }

            /// <remarks/>
            public bool ResourceSubstitutionEnabled
            {
                get
                {
                    return resourceSubstitutionEnabledField;
                }
                set
                {
                    resourceSubstitutionEnabledField = value;
                }
            }

            /// <remarks/>
            public bool LeafOnly
            {
                get
                {
                    return leafOnlyField;
                }
                set
                {
                    leafOnlyField = value;
                }
            }

            /// <remarks/>
            public bool AllLevelsRequired
            {
                get
                {
                    return allLevelsRequiredField;
                }
                set
                {
                    allLevelsRequiredField = value;
                }
            }

            /// <remarks/>
            public bool OnlyTableValuesAllowed
            {
                get
                {
                    return onlyTableValuesAllowedField;
                }
                set
                {
                    onlyTableValuesAllowedField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Mask", IsNullable = false)]
            public ProjectOutlineCodeMask[] Masks
            {
                get
                {
                    return masksField;
                }
                set
                {
                    masksField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectOutlineCodeValue
        {

            private string descriptionField;

            private string valueField;

            private string parentValueIDField;

            private byte typeField;

            private string fieldGUIDField;

            private string valueIDField;

            /// <remarks/>
            public string Description
            {
                get
                {
                    return descriptionField;
                }
                set
                {
                    descriptionField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ParentValueID
            {
                get
                {
                    return parentValueIDField;
                }
                set
                {
                    parentValueIDField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            public string FieldGUID
            {
                get
                {
                    return fieldGUIDField;
                }
                set
                {
                    fieldGUIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueID
            {
                get
                {
                    return valueIDField;
                }
                set
                {
                    valueIDField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectOutlineCodeMask
        {

            private string levelField;

            private byte typeField;

            private string lengthField;

            private string separatorField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Level
            {
                get
                {
                    return levelField;
                }
                set
                {
                    levelField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Length
            {
                get
                {
                    return lengthField;
                }
                set
                {
                    lengthField = value;
                }
            }

            /// <remarks/>
            public string Separator
            {
                get
                {
                    return separatorField;
                }
                set
                {
                    separatorField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectWBSMasks
        {

            private bool verifyUniqueCodesField;

            private bool generateCodesField;

            private string prefixField;

            private ProjectWBSMasksWBSMask[] wBSMaskField;

            /// <remarks/>
            public bool VerifyUniqueCodes
            {
                get
                {
                    return verifyUniqueCodesField;
                }
                set
                {
                    verifyUniqueCodesField = value;
                }
            }

            /// <remarks/>
            public bool GenerateCodes
            {
                get
                {
                    return generateCodesField;
                }
                set
                {
                    generateCodesField = value;
                }
            }

            /// <remarks/>
            public string Prefix
            {
                get
                {
                    return prefixField;
                }
                set
                {
                    prefixField = value;
                }
            }

            /// <remarks/>
            [XmlElement("WBSMask")]
            public ProjectWBSMasksWBSMask[] WBSMask
            {
                get
                {
                    return wBSMaskField;
                }
                set
                {
                    wBSMaskField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectWBSMasksWBSMask
        {

            private string levelField;

            private byte typeField;

            private string lengthField;

            private string separatorField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Level
            {
                get
                {
                    return levelField;
                }
                set
                {
                    levelField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            public string Length
            {
                get
                {
                    return lengthField;
                }
                set
                {
                    lengthField = value;
                }
            }

            /// <remarks/>
            public string Separator
            {
                get
                {
                    return separatorField;
                }
                set
                {
                    separatorField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectExtendedAttribute
        {

            private string fieldIDField;

            private string fieldNameField;

            private byte cFTypeField;

            private string guidField;

            private byte elemTypeField;

            private string maxMultiValuesField;

            private bool userDefField;

            private string aliasField;

            private string secondaryPIDField;

            private bool autoRollDownField;

            private string defaultGuidField;

            private string ltuidField;

            private string phoneticAliasField;

            private byte rollupTypeField;

            private byte calculationTypeField;

            private string formulaField;

            private bool restrictValuesField;

            private byte valuelistSortOrderField;

            private bool appendNewValuesField;

            private string defaultField;

            private ProjectExtendedAttributeValue[] valueListField;

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            public string FieldName
            {
                get
                {
                    return fieldNameField;
                }
                set
                {
                    fieldNameField = value;
                }
            }

            /// <remarks/>
            public byte CFType
            {
                get
                {
                    return cFTypeField;
                }
                set
                {
                    cFTypeField = value;
                }
            }

            /// <remarks/>
            public string Guid
            {
                get
                {
                    return guidField;
                }
                set
                {
                    guidField = value;
                }
            }

            /// <remarks/>
            public byte ElemType
            {
                get
                {
                    return elemTypeField;
                }
                set
                {
                    elemTypeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string MaxMultiValues
            {
                get
                {
                    return maxMultiValuesField;
                }
                set
                {
                    maxMultiValuesField = value;
                }
            }

            /// <remarks/>
            public bool UserDef
            {
                get
                {
                    return userDefField;
                }
                set
                {
                    userDefField = value;
                }
            }

            /// <remarks/>
            public string Alias
            {
                get
                {
                    return aliasField;
                }
                set
                {
                    aliasField = value;
                }
            }

            /// <remarks/>
            public string SecondaryPID
            {
                get
                {
                    return secondaryPIDField;
                }
                set
                {
                    secondaryPIDField = value;
                }
            }

            /// <remarks/>
            public bool AutoRollDown
            {
                get
                {
                    return autoRollDownField;
                }
                set
                {
                    autoRollDownField = value;
                }
            }

            /// <remarks/>
            public string DefaultGuid
            {
                get
                {
                    return defaultGuidField;
                }
                set
                {
                    defaultGuidField = value;
                }
            }

            /// <remarks/>
            public string Ltuid
            {
                get
                {
                    return ltuidField;
                }
                set
                {
                    ltuidField = value;
                }
            }

            /// <remarks/>
            public string PhoneticAlias
            {
                get
                {
                    return phoneticAliasField;
                }
                set
                {
                    phoneticAliasField = value;
                }
            }

            /// <remarks/>
            public byte RollupType
            {
                get
                {
                    return rollupTypeField;
                }
                set
                {
                    rollupTypeField = value;
                }
            }

            /// <remarks/>
            public byte CalculationType
            {
                get
                {
                    return calculationTypeField;
                }
                set
                {
                    calculationTypeField = value;
                }
            }

            /// <remarks/>
            public string Formula
            {
                get
                {
                    return formulaField;
                }
                set
                {
                    formulaField = value;
                }
            }

            /// <remarks/>
            public bool RestrictValues
            {
                get
                {
                    return restrictValuesField;
                }
                set
                {
                    restrictValuesField = value;
                }
            }

            /// <remarks/>
            public byte ValuelistSortOrder
            {
                get
                {
                    return valuelistSortOrderField;
                }
                set
                {
                    valuelistSortOrderField = value;
                }
            }

            /// <remarks/>
            public bool AppendNewValues
            {
                get
                {
                    return appendNewValuesField;
                }
                set
                {
                    appendNewValuesField = value;
                }
            }

            /// <remarks/>
            public string Default
            {
                get
                {
                    return defaultField;
                }
                set
                {
                    defaultField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Value", IsNullable = false)]
            public ProjectExtendedAttributeValue[] ValueList
            {
                get
                {
                    return valueListField;
                }
                set
                {
                    valueListField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectExtendedAttributeValue
        {

            private string phoneticField;

            private string descriptionField;

            private string valueField;

            private string idField;

            /// <remarks/>
            public string Phonetic
            {
                get
                {
                    return phoneticField;
                }
                set
                {
                    phoneticField = value;
                }
            }

            /// <remarks/>
            public string Description
            {
                get
                {
                    return descriptionField;
                }
                set
                {
                    descriptionField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ID
            {
                get
                {
                    return idField;
                }
                set
                {
                    idField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendar
        {

            private string uIDField;

            private string nameField;

            private bool isBaseCalendarField;

            private string baseCalendarUIDField;

            private ProjectCalendarWeekDay[] weekDaysField;

            private ProjectCalendarException[] exceptionsField;

            private ProjectCalendarWorkWeek[] workWeeksField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            public bool IsBaseCalendar
            {
                get
                {
                    return isBaseCalendarField;
                }
                set
                {
                    isBaseCalendarField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string BaseCalendarUID
            {
                get
                {
                    return baseCalendarUIDField;
                }
                set
                {
                    baseCalendarUIDField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("WeekDay", IsNullable = false)]
            public ProjectCalendarWeekDay[] WeekDays
            {
                get
                {
                    return weekDaysField;
                }
                set
                {
                    weekDaysField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Exception", IsNullable = false)]
            public ProjectCalendarException[] Exceptions
            {
                get
                {
                    return exceptionsField;
                }
                set
                {
                    exceptionsField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("WorkWeek", IsNullable = false)]
            public ProjectCalendarWorkWeek[] WorkWeeks
            {
                get
                {
                    return workWeeksField;
                }
                set
                {
                    workWeeksField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarWeekDay
        {

            private byte dayTypeField;

            private bool dayWorkingField;

            private ProjectCalendarWeekDayTimePeriod timePeriodField;

            private ProjectCalendarWeekDayWorkingTime[] workingTimesField;

            /// <remarks/>
            public byte DayType
            {
                get
                {
                    return dayTypeField;
                }
                set
                {
                    dayTypeField = value;
                }
            }

            /// <remarks/>
            public bool DayWorking
            {
                get
                {
                    return dayWorkingField;
                }
                set
                {
                    dayWorkingField = value;
                }
            }

            /// <remarks/>
            public ProjectCalendarWeekDayTimePeriod TimePeriod
            {
                get
                {
                    return timePeriodField;
                }
                set
                {
                    timePeriodField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("WorkingTime", IsNullable = false)]
            public ProjectCalendarWeekDayWorkingTime[] WorkingTimes
            {
                get
                {
                    return workingTimesField;
                }
                set
                {
                    workingTimesField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarWeekDayTimePeriod
        {

            private DateTime fromDateField;

            private DateTime toDateField;

            /// <remarks/>
            public DateTime FromDate
            {
                get
                {
                    return fromDateField;
                }
                set
                {
                    fromDateField = value;
                }
            }

            /// <remarks/>
            public DateTime ToDate
            {
                get
                {
                    return toDateField;
                }
                set
                {
                    toDateField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarWeekDayWorkingTime
        {

            private DateTime fromTimeField;

            private DateTime toTimeField;

            /// <remarks/>
            [XmlElement(DataType = "time")]
            public DateTime FromTime
            {
                get
                {
                    return fromTimeField;
                }
                set
                {
                    fromTimeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "time")]
            public DateTime ToTime
            {
                get
                {
                    return toTimeField;
                }
                set
                {
                    toTimeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarException
        {

            private bool enteredByOccurrencesField;

            private ProjectCalendarExceptionTimePeriod timePeriodField;

            private string occurrencesField;

            private string nameField;

            private byte typeField;

            private string periodField;

            private string daysOfWeekField;

            private byte monthItemField;

            private byte monthPositionField;

            private byte monthField;

            private string monthDayField;

            private bool dayWorkingField;

            private ProjectCalendarExceptionWorkingTime[] workingTimesField;

            /// <remarks/>
            public bool EnteredByOccurrences
            {
                get
                {
                    return enteredByOccurrencesField;
                }
                set
                {
                    enteredByOccurrencesField = value;
                }
            }

            /// <remarks/>
            public ProjectCalendarExceptionTimePeriod TimePeriod
            {
                get
                {
                    return timePeriodField;
                }
                set
                {
                    timePeriodField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Occurrences
            {
                get
                {
                    return occurrencesField;
                }
                set
                {
                    occurrencesField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Period
            {
                get
                {
                    return periodField;
                }
                set
                {
                    periodField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string DaysOfWeek
            {
                get
                {
                    return daysOfWeekField;
                }
                set
                {
                    daysOfWeekField = value;
                }
            }

            /// <remarks/>
            public byte MonthItem
            {
                get
                {
                    return monthItemField;
                }
                set
                {
                    monthItemField = value;
                }
            }

            /// <remarks/>
            public byte MonthPosition
            {
                get
                {
                    return monthPositionField;
                }
                set
                {
                    monthPositionField = value;
                }
            }

            /// <remarks/>
            public byte Month
            {
                get
                {
                    return monthField;
                }
                set
                {
                    monthField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string MonthDay
            {
                get
                {
                    return monthDayField;
                }
                set
                {
                    monthDayField = value;
                }
            }

            /// <remarks/>
            public bool DayWorking
            {
                get
                {
                    return dayWorkingField;
                }
                set
                {
                    dayWorkingField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("WorkingTime", IsNullable = false)]
            public ProjectCalendarExceptionWorkingTime[] WorkingTimes
            {
                get
                {
                    return workingTimesField;
                }
                set
                {
                    workingTimesField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarExceptionTimePeriod
        {

            private DateTime fromDateField;

            private DateTime toDateField;

            /// <remarks/>
            public DateTime FromDate
            {
                get
                {
                    return fromDateField;
                }
                set
                {
                    fromDateField = value;
                }
            }

            /// <remarks/>
            public DateTime ToDate
            {
                get
                {
                    return toDateField;
                }
                set
                {
                    toDateField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarExceptionWorkingTime
        {

            private DateTime fromTimeField;

            private DateTime toTimeField;

            /// <remarks/>
            [XmlElement(DataType = "time")]
            public DateTime FromTime
            {
                get
                {
                    return fromTimeField;
                }
                set
                {
                    fromTimeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "time")]
            public DateTime ToTime
            {
                get
                {
                    return toTimeField;
                }
                set
                {
                    toTimeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarWorkWeek
        {

            private ProjectCalendarWorkWeekTimePeriod timePeriodField;

            private string nameField;

            private ProjectCalendarWorkWeekWeekDay[] weekDayField;

            /// <remarks/>
            
            public ProjectCalendarWorkWeekTimePeriod TimePeriod
            {
                get
                {
                    return timePeriodField;
                }
                set
                {
                    timePeriodField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            [XmlElement("WeekDay")]
            [XmlIgnore]
            public ProjectCalendarWorkWeekWeekDay[] WeekDay
            {
                get
                {
                    return weekDayField;
                }
                set
                {
                    weekDayField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarWorkWeekTimePeriod
        {

            private DateTime fromDateField;

            private DateTime toDateField;

            /// <remarks/>
            public DateTime FromDate
            {
                get
                {
                    return fromDateField;
                }
                set
                {
                    fromDateField = value;
                }
            }

            /// <remarks/>
            public DateTime ToDate
            {
                get
                {
                    return toDateField;
                }
                set
                {
                    toDateField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectCalendarWorkWeekWeekDay
        {

            private byte dayTypeField;

            private bool dayWorkingField;

            /// <remarks/>
            public byte DayType
            {
                get
                {
                    return dayTypeField;
                }
                set
                {
                    dayTypeField = value;
                }
            }

            /// <remarks/>
            public bool DayWorking
            {
                get
                {
                    return dayWorkingField;
                }
                set
                {
                    dayWorkingField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTask
        {

            private string uIDField;

            private string idField;

            private string nameField;

            private byte typeField;

            private bool isNullField;

            private DateTime createDateField;

            private string contactField;

            private string wBSField;

            private string wBSLevelField;

            private string outlineNumberField;

            private string outlineLevelField;

            private string priorityField;

            private DateTime startField;

            private DateTime finishField;

            private string durationField;

            private byte durationFormatField;

            private string workField;

            private DateTime stopField;

            private DateTime resumeField;

            private bool resumeValidField;

            private bool effortDrivenField;

            private bool recurringField;

            private bool overAllocatedField;

            private bool estimatedField;

            private bool milestoneField;

            private bool summaryField;

            private bool criticalField;

            private bool isSubprojectField;

            private bool isSubprojectReadOnlyField;

            private string subprojectNameField;

            private bool externalTaskField;

            private string externalTaskProjectField;

            private DateTime earlyStartField;

            private DateTime earlyFinishField;

            private DateTime lateStartField;

            private DateTime lateFinishField;

            private string startVarianceField;

            private string finishVarianceField;

            private float workVarianceField;

            private string freeSlackField;

            private string totalSlackField;

            private float fixedCostField;

            private byte fixedCostAccrualField;

            private string percentCompleteField;

            private string percentWorkCompleteField;

            private string costField;

            private string overtimeCostField;

            private string overtimeWorkField;

            private DateTime actualStartField;

            private DateTime actualFinishField;

            private string actualDurationField;

            private string actualCostField;

            private string actualOvertimeCostField;

            private string actualWorkField;

            private string actualOvertimeWorkField;

            private string regularWorkField;

            private string remainingDurationField;

            private string remainingCostField;

            private string remainingWorkField;

            private string remainingOvertimeCostField;

            private string remainingOvertimeWorkField;

            private float aCWPField;

            private float cvField;

            private byte constraintTypeField;

            private string calendarUIDField;

            private DateTime constraintDateField;

            private DateTime deadlineField;

            private bool levelAssignmentsField;

            private bool levelingCanSplitField;

            private string levelingDelayField;

            private byte levelingDelayFormatField;

            private DateTime preLeveledStartField;

            private DateTime preLeveledFinishField;

            private string hyperlinkField;

            private string hyperlinkAddressField;

            private string hyperlinkSubAddressField;

            private bool ignoreResourceCalendarField;

            private string notesField;

            private bool hideBarField;

            private bool rollupField;

            private float bCWSField;

            private float bCWPField;

            private string physicalPercentCompleteField;

            private byte earnedValueMethodField;

            private ProjectTaskPredecessorLink[] predecessorLinkField;

            private string actualWorkProtectedField;

            private string actualOvertimeWorkProtectedField;

            private ProjectTaskExtendedAttribute[] extendedAttributeField;

            private ProjectTaskBaseline[] baselineField;

            private ProjectTaskOutlineCode[] outlineCodeField;

            private bool isPublishedField;

            private string statusManagerField;

            private DateTime commitmentStartField;

            private DateTime commitmentFinishField;

            private byte commitmentTypeField;

            private ProjectTaskTimephasedData[] timephasedDataField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ID
            {
                get
                {
                    return idField;
                }
                set
                {
                    idField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            public bool IsNull
            {
                get
                {
                    return isNullField;
                }
                set
                {
                    isNullField = value;
                }
            }

            /// <remarks/>
            public DateTime CreateDate
            {
                get
                {
                    return createDateField;
                }
                set
                {
                    createDateField = value;
                }
            }

            /// <remarks/>
            public string Contact
            {
                get
                {
                    return contactField;
                }
                set
                {
                    contactField = value;
                }
            }

            /// <remarks/>
            public string WBS
            {
                get
                {
                    return wBSField;
                }
                set
                {
                    wBSField = value;
                }
            }

            /// <remarks/>
            public string WBSLevel
            {
                get
                {
                    return wBSLevelField;
                }
                set
                {
                    wBSLevelField = value;
                }
            }

            /// <remarks/>
            public string OutlineNumber
            {
                get
                {
                    return outlineNumberField;
                }
                set
                {
                    outlineNumberField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string OutlineLevel
            {
                get
                {
                    return outlineLevelField;
                }
                set
                {
                    outlineLevelField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Priority
            {
                get
                {
                    return priorityField;
                }
                set
                {
                    priorityField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Duration
            {
                get
                {
                    return durationField;
                }
                set
                {
                    durationField = value;
                }
            }

            /// <remarks/>
            public byte DurationFormat
            {
                get
                {
                    return durationFormatField;
                }
                set
                {
                    durationFormatField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Work
            {
                get
                {
                    return workField;
                }
                set
                {
                    workField = value;
                }
            }

            /// <remarks/>
            public DateTime Stop
            {
                get
                {
                    return stopField;
                }
                set
                {
                    stopField = value;
                }
            }

            /// <remarks/>
            public DateTime Resume
            {
                get
                {
                    return resumeField;
                }
                set
                {
                    resumeField = value;
                }
            }

            /// <remarks/>
            public bool ResumeValid
            {
                get
                {
                    return resumeValidField;
                }
                set
                {
                    resumeValidField = value;
                }
            }

            /// <remarks/>
            public bool EffortDriven
            {
                get
                {
                    return effortDrivenField;
                }
                set
                {
                    effortDrivenField = value;
                }
            }

            /// <remarks/>
            public bool Recurring
            {
                get
                {
                    return recurringField;
                }
                set
                {
                    recurringField = value;
                }
            }

            /// <remarks/>
            public bool OverAllocated
            {
                get
                {
                    return overAllocatedField;
                }
                set
                {
                    overAllocatedField = value;
                }
            }

            /// <remarks/>
            public bool Estimated
            {
                get
                {
                    return estimatedField;
                }
                set
                {
                    estimatedField = value;
                }
            }

            /// <remarks/>
            public bool Milestone
            {
                get
                {
                    return milestoneField;
                }
                set
                {
                    milestoneField = value;
                }
            }

            /// <remarks/>
            public bool Summary
            {
                get
                {
                    return summaryField;
                }
                set
                {
                    summaryField = value;
                }
            }

            /// <remarks/>
            public bool Critical
            {
                get
                {
                    return criticalField;
                }
                set
                {
                    criticalField = value;
                }
            }

            /// <remarks/>
            public bool IsSubproject
            {
                get
                {
                    return isSubprojectField;
                }
                set
                {
                    isSubprojectField = value;
                }
            }

            /// <remarks/>
            public bool IsSubprojectReadOnly
            {
                get
                {
                    return isSubprojectReadOnlyField;
                }
                set
                {
                    isSubprojectReadOnlyField = value;
                }
            }

            /// <remarks/>
            public string SubprojectName
            {
                get
                {
                    return subprojectNameField;
                }
                set
                {
                    subprojectNameField = value;
                }
            }

            /// <remarks/>
            public bool ExternalTask
            {
                get
                {
                    return externalTaskField;
                }
                set
                {
                    externalTaskField = value;
                }
            }

            /// <remarks/>
            public string ExternalTaskProject
            {
                get
                {
                    return externalTaskProjectField;
                }
                set
                {
                    externalTaskProjectField = value;
                }
            }

            /// <remarks/>
            [XmlIgnore]
            public DateTime EarlyStart
            {
                get
                {
                    return earlyStartField;
                }
                set
                {
                    earlyStartField = value;
                }
            }

            /// <remarks/>

            [XmlIgnore]
            public DateTime EarlyFinish
            {
                get
                {
                    return earlyFinishField;
                }
                set
                {
                    earlyFinishField = value;
                }
            }

            /// <remarks/>
            [XmlIgnore]
            public DateTime LateStart
            {
                get
                {
                    return lateStartField;
                }
                set
                {
                    lateStartField = value;
                }
            }

            /// <remarks/>
            [XmlIgnore]
            public DateTime LateFinish
            {
                get
                {
                    return lateFinishField;
                }
                set
                {
                    lateFinishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string StartVariance
            {
                get
                {
                    return startVarianceField;
                }
                set
                {
                    startVarianceField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string FinishVariance
            {
                get
                {
                    return finishVarianceField;
                }
                set
                {
                    finishVarianceField = value;
                }
            }

            /// <remarks/>
            public float WorkVariance
            {
                get
                {
                    return workVarianceField;
                }
                set
                {
                    workVarianceField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string FreeSlack
            {
                get
                {
                    return freeSlackField;
                }
                set
                {
                    freeSlackField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string TotalSlack
            {
                get
                {
                    return totalSlackField;
                }
                set
                {
                    totalSlackField = value;
                }
            }

            /// <remarks/>
            public float FixedCost
            {
                get
                {
                    return fixedCostField;
                }
                set
                {
                    fixedCostField = value;
                }
            }

            /// <remarks/>
            public byte FixedCostAccrual
            {
                get
                {
                    return fixedCostAccrualField;
                }
                set
                {
                    fixedCostAccrualField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string PercentComplete
            {
                get
                {
                    return percentCompleteField;
                }
                set
                {
                    percentCompleteField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string PercentWorkComplete
            {
                get
                {
                    return percentWorkCompleteField;
                }
                set
                {
                    percentWorkCompleteField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Cost
            {
                get
                {
                    return costField;
                }
                set
                {
                    costField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string OvertimeCost
            {
                get
                {
                    return overtimeCostField;
                }
                set
                {
                    overtimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string OvertimeWork
            {
                get
                {
                    return overtimeWorkField;
                }
                set
                {
                    overtimeWorkField = value;
                }
            }

            /// <remarks/>
            public DateTime ActualStart
            {
                get
                {
                    return actualStartField;
                }
                set
                {
                    actualStartField = value;
                }
            }

            /// <remarks/>
            public DateTime ActualFinish
            {
                get
                {
                    return actualFinishField;
                }
                set
                {
                    actualFinishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualDuration
            {
                get
                {
                    return actualDurationField;
                }
                set
                {
                    actualDurationField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ActualCost
            {
                get
                {
                    return actualCostField;
                }
                set
                {
                    actualCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ActualOvertimeCost
            {
                get
                {
                    return actualOvertimeCostField;
                }
                set
                {
                    actualOvertimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualWork
            {
                get
                {
                    return actualWorkField;
                }
                set
                {
                    actualWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualOvertimeWork
            {
                get
                {
                    return actualOvertimeWorkField;
                }
                set
                {
                    actualOvertimeWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RegularWork
            {
                get
                {
                    return regularWorkField;
                }
                set
                {
                    regularWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingDuration
            {
                get
                {
                    return remainingDurationField;
                }
                set
                {
                    remainingDurationField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string RemainingCost
            {
                get
                {
                    return remainingCostField;
                }
                set
                {
                    remainingCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingWork
            {
                get
                {
                    return remainingWorkField;
                }
                set
                {
                    remainingWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string RemainingOvertimeCost
            {
                get
                {
                    return remainingOvertimeCostField;
                }
                set
                {
                    remainingOvertimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingOvertimeWork
            {
                get
                {
                    return remainingOvertimeWorkField;
                }
                set
                {
                    remainingOvertimeWorkField = value;
                }
            }

            /// <remarks/>
            public float ACWP
            {
                get
                {
                    return aCWPField;
                }
                set
                {
                    aCWPField = value;
                }
            }

            /// <remarks/>
            public float CV
            {
                get
                {
                    return cvField;
                }
                set
                {
                    cvField = value;
                }
            }

            /// <remarks/>
            public byte ConstraintType
            {
                get
                {
                    return constraintTypeField;
                }
                set
                {
                    constraintTypeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string CalendarUID
            {
                get
                {
                    return calendarUIDField;
                }
                set
                {
                    calendarUIDField = value;
                }
            }

            /// <remarks/>
            public DateTime ConstraintDate
            {
                get
                {
                    return constraintDateField;
                }
                set
                {
                    constraintDateField = value;
                }
            }

            /// <remarks/>
            public DateTime Deadline
            {
                get
                {
                    return deadlineField;
                }
                set
                {
                    deadlineField = value;
                }
            }

            /// <remarks/>
            public bool LevelAssignments
            {
                get
                {
                    return levelAssignmentsField;
                }
                set
                {
                    levelAssignmentsField = value;
                }
            }

            /// <remarks/>
            public bool LevelingCanSplit
            {
                get
                {
                    return levelingCanSplitField;
                }
                set
                {
                    levelingCanSplitField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string LevelingDelay
            {
                get
                {
                    return levelingDelayField;
                }
                set
                {
                    levelingDelayField = value;
                }
            }

            /// <remarks/>
            public byte LevelingDelayFormat
            {
                get
                {
                    return levelingDelayFormatField;
                }
                set
                {
                    levelingDelayFormatField = value;
                }
            }

            /// <remarks/>
            public DateTime PreLeveledStart
            {
                get
                {
                    return preLeveledStartField;
                }
                set
                {
                    preLeveledStartField = value;
                }
            }

            /// <remarks/>
            public DateTime PreLeveledFinish
            {
                get
                {
                    return preLeveledFinishField;
                }
                set
                {
                    preLeveledFinishField = value;
                }
            }

            /// <remarks/>
            public string Hyperlink
            {
                get
                {
                    return hyperlinkField;
                }
                set
                {
                    hyperlinkField = value;
                }
            }

            /// <remarks/>
            public string HyperlinkAddress
            {
                get
                {
                    return hyperlinkAddressField;
                }
                set
                {
                    hyperlinkAddressField = value;
                }
            }

            /// <remarks/>
            public string HyperlinkSubAddress
            {
                get
                {
                    return hyperlinkSubAddressField;
                }
                set
                {
                    hyperlinkSubAddressField = value;
                }
            }

            /// <remarks/>
            public bool IgnoreResourceCalendar
            {
                get
                {
                    return ignoreResourceCalendarField;
                }
                set
                {
                    ignoreResourceCalendarField = value;
                }
            }

            /// <remarks/>
            public string Notes
            {
                get
                {
                    return notesField;
                }
                set
                {
                    notesField = value;
                }
            }

            /// <remarks/>
            public bool HideBar
            {
                get
                {
                    return hideBarField;
                }
                set
                {
                    hideBarField = value;
                }
            }

            /// <remarks/>
            public bool Rollup
            {
                get
                {
                    return rollupField;
                }
                set
                {
                    rollupField = value;
                }
            }

            /// <remarks/>
            public float BCWS
            {
                get
                {
                    return bCWSField;
                }
                set
                {
                    bCWSField = value;
                }
            }

            /// <remarks/>
            public float BCWP
            {
                get
                {
                    return bCWPField;
                }
                set
                {
                    bCWPField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string PhysicalPercentComplete
            {
                get
                {
                    return physicalPercentCompleteField;
                }
                set
                {
                    physicalPercentCompleteField = value;
                }
            }

            /// <remarks/>
            public byte EarnedValueMethod
            {
                get
                {
                    return earnedValueMethodField;
                }
                set
                {
                    earnedValueMethodField = value;
                }
            }

            /// <remarks/>
            [XmlElement("PredecessorLink")]
            public ProjectTaskPredecessorLink[] PredecessorLink
            {
                get
                {
                    return predecessorLinkField;
                }
                set
                {
                    predecessorLinkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualWorkProtected
            {
                get
                {
                    return actualWorkProtectedField;
                }
                set
                {
                    actualWorkProtectedField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualOvertimeWorkProtected
            {
                get
                {
                    return actualOvertimeWorkProtectedField;
                }
                set
                {
                    actualOvertimeWorkProtectedField = value;
                }
            }

            /// <remarks/>
            [XmlElement("ExtendedAttribute")]
            public ProjectTaskExtendedAttribute[] ExtendedAttribute
            {
                get
                {
                    return extendedAttributeField;
                }
                set
                {
                    extendedAttributeField = value;
                }
            }

            /// <remarks/>
            [XmlElement("Baseline")]
            public ProjectTaskBaseline[] Baseline
            {
                get
                {
                    return baselineField;
                }
                set
                {
                    baselineField = value;
                }
            }

            /// <remarks/>
            [XmlElement("OutlineCode")]
            public ProjectTaskOutlineCode[] OutlineCode
            {
                get
                {
                    return outlineCodeField;
                }
                set
                {
                    outlineCodeField = value;
                }
            }

            /// <remarks/>
            public bool IsPublished
            {
                get
                {
                    return isPublishedField;
                }
                set
                {
                    isPublishedField = value;
                }
            }

            /// <remarks/>
            public string StatusManager
            {
                get
                {
                    return statusManagerField;
                }
                set
                {
                    statusManagerField = value;
                }
            }

            /// <remarks/>
            public DateTime CommitmentStart
            {
                get
                {
                    return commitmentStartField;
                }
                set
                {
                    commitmentStartField = value;
                }
            }

            /// <remarks/>
            public DateTime CommitmentFinish
            {
                get
                {
                    return commitmentFinishField;
                }
                set
                {
                    commitmentFinishField = value;
                }
            }

            /// <remarks/>
            public byte CommitmentType
            {
                get
                {
                    return commitmentTypeField;
                }
                set
                {
                    commitmentTypeField = value;
                }
            }

            /// <remarks/>
            [XmlElement("TimephasedData")]
            public ProjectTaskTimephasedData[] TimephasedData
            {
                get
                {
                    return timephasedDataField;
                }
                set
                {
                    timephasedDataField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTaskPredecessorLink
        {

            private string predecessorUIDField;

            private byte typeField;

            private bool crossProjectField;

            private string crossProjectNameField;

            private string linkLagField;

            private byte lagFormatField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string PredecessorUID
            {
                get
                {
                    return predecessorUIDField;
                }
                set
                {
                    predecessorUIDField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            public bool CrossProject
            {
                get
                {
                    return crossProjectField;
                }
                set
                {
                    crossProjectField = value;
                }
            }

            /// <remarks/>
            public string CrossProjectName
            {
                get
                {
                    return crossProjectNameField;
                }
                set
                {
                    crossProjectNameField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string LinkLag
            {
                get
                {
                    return linkLagField;
                }
                set
                {
                    linkLagField = value;
                }
            }

            /// <remarks/>
            public byte LagFormat
            {
                get
                {
                    return lagFormatField;
                }
                set
                {
                    lagFormatField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTaskExtendedAttribute
        {

            private string fieldIDField;

            private string valueField;

            private string valueGUIDField;

            private byte durationFormatField;

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueGUID
            {
                get
                {
                    return valueGUIDField;
                }
                set
                {
                    valueGUIDField = value;
                }
            }

            /// <remarks/>
            public byte DurationFormat
            {
                get
                {
                    return durationFormatField;
                }
                set
                {
                    durationFormatField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTaskBaseline
        {

            private ProjectTaskBaselineTimephasedData[] timephasedDataField;

            private string numberField;

            private bool interimField;

            private DateTime startField;

            private DateTime finishField;

            private string durationField;

            private byte durationFormatField;

            private bool estimatedDurationField;

            private string workField;

            private decimal costField;

            private float bCWSField;

            private float bCWPField;

            private float fixedCostField;

            /// <remarks/>
            [XmlElement("TimephasedData")]
            public ProjectTaskBaselineTimephasedData[] TimephasedData
            {
                get
                {
                    return timephasedDataField;
                }
                set
                {
                    timephasedDataField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Number
            {
                get
                {
                    return numberField;
                }
                set
                {
                    numberField = value;
                }
            }

            /// <remarks/>
            public bool Interim
            {
                get
                {
                    return interimField;
                }
                set
                {
                    interimField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Duration
            {
                get
                {
                    return durationField;
                }
                set
                {
                    durationField = value;
                }
            }

            /// <remarks/>
            public byte DurationFormat
            {
                get
                {
                    return durationFormatField;
                }
                set
                {
                    durationFormatField = value;
                }
            }

            /// <remarks/>
            public bool EstimatedDuration
            {
                get
                {
                    return estimatedDurationField;
                }
                set
                {
                    estimatedDurationField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Work
            {
                get
                {
                    return workField;
                }
                set
                {
                    workField = value;
                }
            }

            /// <remarks/>
            public decimal Cost
            {
                get
                {
                    return costField;
                }
                set
                {
                    costField = value;
                }
            }

            /// <remarks/>
            public float BCWS
            {
                get
                {
                    return bCWSField;
                }
                set
                {
                    bCWSField = value;
                }
            }

            /// <remarks/>
            public float BCWP
            {
                get
                {
                    return bCWPField;
                }
                set
                {
                    bCWPField = value;
                }
            }

            /// <remarks/>
            public float FixedCost
            {
                get
                {
                    return fixedCostField;
                }
                set
                {
                    fixedCostField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTaskBaselineTimephasedData
        {

            private byte typeField;

            private string uIDField;

            private DateTime startField;

            private DateTime finishField;

            private byte unitField;

            private string valueField;

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            public byte Unit
            {
                get
                {
                    return unitField;
                }
                set
                {
                    unitField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTaskOutlineCode
        {

            private string fieldIDField;

            private string valueIDField;

            private string valueGUIDField;

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueID
            {
                get
                {
                    return valueIDField;
                }
                set
                {
                    valueIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueGUID
            {
                get
                {
                    return valueGUIDField;
                }
                set
                {
                    valueGUIDField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectTaskTimephasedData
        {

            private byte typeField;

            private string uIDField;

            private DateTime startField;

            private DateTime finishField;

            private byte unitField;

            private string valueField;

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            public byte Unit
            {
                get
                {
                    return unitField;
                }
                set
                {
                    unitField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResource
        {

            private string uIDField;

            private string idField;

            private string nameField;

            private byte typeField;

            private bool isNullField;

            private string initialsField;

            private string phoneticsField;

            private string nTAccountField;

            private string materialLabelField;

            private string codeField;

            private string groupField;

            private byte workGroupField;

            private string emailAddressField;

            private string hyperlinkField;

            private string hyperlinkAddressField;

            private string hyperlinkSubAddressField;

            private float maxUnitsField;

            private float peakUnitsField;

            private bool overAllocatedField;

            private DateTime availableFromField;

            private DateTime availableToField;

            private DateTime startField;

            private DateTime finishField;

            private bool canLevelField;

            private byte accrueAtField;

            private string workField;

            private string regularWorkField;

            private string overtimeWorkField;

            private string actualWorkField;

            private string remainingWorkField;

            private string actualOvertimeWorkField;

            private string remainingOvertimeWorkField;

            private string percentWorkCompleteField;

            private string standardRateField;

            private byte standardRateFormatField;

            private string costField;

            private string overtimeRateField;

            private byte overtimeRateFormatField;

            private string overtimeCostField;

            private string costPerUseField;

            private string actualCostField;

            private string actualOvertimeCostField;

            private string remainingCostField;

            private string remainingOvertimeCostField;

            private float workVarianceField;

            private float costVarianceField;

            private float svField;

            private float cvField;

            private float aCWPField;

            private string calendarUIDField;

            private string notesField;

            private float bCWSField;

            private float bCWPField;

            private bool isGenericField;

            private bool isInactiveField;

            private bool isEnterpriseField;

            private byte bookingTypeField;

            private string actualWorkProtectedField;

            private string actualOvertimeWorkProtectedField;

            private string activeDirectoryGUIDField;

            private DateTime creationDateField;

            private ProjectResourceExtendedAttribute[] extendedAttributeField;

            private ProjectResourceBaseline[] baselineField;

            private ProjectResourceOutlineCode[] outlineCodeField;

            private bool isCostResourceField;

            private string assnOwnerField;

            private string assnOwnerGuidField;

            private bool isBudgetField;

            private ProjectResourceAvailabilityPeriod[] availabilityPeriodsField;

            private ProjectResourceRate[] ratesField;

            private ProjectResourceTimephasedData[] timephasedDataField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ID
            {
                get
                {
                    return idField;
                }
                set
                {
                    idField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            public bool IsNull
            {
                get
                {
                    return isNullField;
                }
                set
                {
                    isNullField = value;
                }
            }

            /// <remarks/>
            public string Initials
            {
                get
                {
                    return initialsField;
                }
                set
                {
                    initialsField = value;
                }
            }

            /// <remarks/>
            public string Phonetics
            {
                get
                {
                    return phoneticsField;
                }
                set
                {
                    phoneticsField = value;
                }
            }

            /// <remarks/>
            public string NTAccount
            {
                get
                {
                    return nTAccountField;
                }
                set
                {
                    nTAccountField = value;
                }
            }

            /// <remarks/>
            public string MaterialLabel
            {
                get
                {
                    return materialLabelField;
                }
                set
                {
                    materialLabelField = value;
                }
            }

            /// <remarks/>
            public string Code
            {
                get
                {
                    return codeField;
                }
                set
                {
                    codeField = value;
                }
            }

            /// <remarks/>
            public string Group
            {
                get
                {
                    return groupField;
                }
                set
                {
                    groupField = value;
                }
            }

            /// <remarks/>
            public byte WorkGroup
            {
                get
                {
                    return workGroupField;
                }
                set
                {
                    workGroupField = value;
                }
            }

            /// <remarks/>
            public string EmailAddress
            {
                get
                {
                    return emailAddressField;
                }
                set
                {
                    emailAddressField = value;
                }
            }

            /// <remarks/>
            public string Hyperlink
            {
                get
                {
                    return hyperlinkField;
                }
                set
                {
                    hyperlinkField = value;
                }
            }

            /// <remarks/>
            public string HyperlinkAddress
            {
                get
                {
                    return hyperlinkAddressField;
                }
                set
                {
                    hyperlinkAddressField = value;
                }
            }

            /// <remarks/>
            public string HyperlinkSubAddress
            {
                get
                {
                    return hyperlinkSubAddressField;
                }
                set
                {
                    hyperlinkSubAddressField = value;
                }
            }

            /// <remarks/>
            public float MaxUnits
            {
                get
                {
                    return maxUnitsField;
                }
                set
                {
                    maxUnitsField = value;
                }
            }

            /// <remarks/>
            public float PeakUnits
            {
                get
                {
                    return peakUnitsField;
                }
                set
                {
                    peakUnitsField = value;
                }
            }

            /// <remarks/>
            public bool OverAllocated
            {
                get
                {
                    return overAllocatedField;
                }
                set
                {
                    overAllocatedField = value;
                }
            }

            /// <remarks/>
            public DateTime AvailableFrom
            {
                get
                {
                    return availableFromField;
                }
                set
                {
                    availableFromField = value;
                }
            }

            /// <remarks/>
            public DateTime AvailableTo
            {
                get
                {
                    return availableToField;
                }
                set
                {
                    availableToField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            public bool CanLevel
            {
                get
                {
                    return canLevelField;
                }
                set
                {
                    canLevelField = value;
                }
            }

            /// <remarks/>
            public byte AccrueAt
            {
                get
                {
                    return accrueAtField;
                }
                set
                {
                    accrueAtField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Work
            {
                get
                {
                    return workField;
                }
                set
                {
                    workField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RegularWork
            {
                get
                {
                    return regularWorkField;
                }
                set
                {
                    regularWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string OvertimeWork
            {
                get
                {
                    return overtimeWorkField;
                }
                set
                {
                    overtimeWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualWork
            {
                get
                {
                    return actualWorkField;
                }
                set
                {
                    actualWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingWork
            {
                get
                {
                    return remainingWorkField;
                }
                set
                {
                    remainingWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualOvertimeWork
            {
                get
                {
                    return actualOvertimeWorkField;
                }
                set
                {
                    actualOvertimeWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingOvertimeWork
            {
                get
                {
                    return remainingOvertimeWorkField;
                }
                set
                {
                    remainingOvertimeWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string PercentWorkComplete
            {
                get
                {
                    return percentWorkCompleteField;
                }
                set
                {
                    percentWorkCompleteField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string StandardRate
            {
                get
                {
                    return standardRateField;
                }
                set
                {
                    standardRateField = value;
                }
            }

            /// <remarks/>
            public byte StandardRateFormat
            {
                get
                {
                    return standardRateFormatField;
                }
                set
                {
                    standardRateFormatField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Cost
            {
                get
                {
                    return costField;
                }
                set
                {
                    costField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string OvertimeRate
            {
                get
                {
                    return overtimeRateField;
                }
                set
                {
                    overtimeRateField = value;
                }
            }

            /// <remarks/>
            public byte OvertimeRateFormat
            {
                get
                {
                    return overtimeRateFormatField;
                }
                set
                {
                    overtimeRateFormatField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string OvertimeCost
            {
                get
                {
                    return overtimeCostField;
                }
                set
                {
                    overtimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string CostPerUse
            {
                get
                {
                    return costPerUseField;
                }
                set
                {
                    costPerUseField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ActualCost
            {
                get
                {
                    return actualCostField;
                }
                set
                {
                    actualCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ActualOvertimeCost
            {
                get
                {
                    return actualOvertimeCostField;
                }
                set
                {
                    actualOvertimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string RemainingCost
            {
                get
                {
                    return remainingCostField;
                }
                set
                {
                    remainingCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string RemainingOvertimeCost
            {
                get
                {
                    return remainingOvertimeCostField;
                }
                set
                {
                    remainingOvertimeCostField = value;
                }
            }

            /// <remarks/>
            public float WorkVariance
            {
                get
                {
                    return workVarianceField;
                }
                set
                {
                    workVarianceField = value;
                }
            }

            /// <remarks/>
            public float CostVariance
            {
                get
                {
                    return costVarianceField;
                }
                set
                {
                    costVarianceField = value;
                }
            }

            /// <remarks/>
            public float SV
            {
                get
                {
                    return svField;
                }
                set
                {
                    svField = value;
                }
            }

            /// <remarks/>
            public float CV
            {
                get
                {
                    return cvField;
                }
                set
                {
                    cvField = value;
                }
            }

            /// <remarks/>
            public float ACWP
            {
                get
                {
                    return aCWPField;
                }
                set
                {
                    aCWPField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string CalendarUID
            {
                get
                {
                    return calendarUIDField;
                }
                set
                {
                    calendarUIDField = value;
                }
            }

            /// <remarks/>
            public string Notes
            {
                get
                {
                    return notesField;
                }
                set
                {
                    notesField = value;
                }
            }

            /// <remarks/>
            public float BCWS
            {
                get
                {
                    return bCWSField;
                }
                set
                {
                    bCWSField = value;
                }
            }

            /// <remarks/>
            public float BCWP
            {
                get
                {
                    return bCWPField;
                }
                set
                {
                    bCWPField = value;
                }
            }

            /// <remarks/>
            public bool IsGeneric
            {
                get
                {
                    return isGenericField;
                }
                set
                {
                    isGenericField = value;
                }
            }

            /// <remarks/>
            public bool IsInactive
            {
                get
                {
                    return isInactiveField;
                }
                set
                {
                    isInactiveField = value;
                }
            }

            /// <remarks/>
            public bool IsEnterprise
            {
                get
                {
                    return isEnterpriseField;
                }
                set
                {
                    isEnterpriseField = value;
                }
            }

            /// <remarks/>
            public byte BookingType
            {
                get
                {
                    return bookingTypeField;
                }
                set
                {
                    bookingTypeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualWorkProtected
            {
                get
                {
                    return actualWorkProtectedField;
                }
                set
                {
                    actualWorkProtectedField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualOvertimeWorkProtected
            {
                get
                {
                    return actualOvertimeWorkProtectedField;
                }
                set
                {
                    actualOvertimeWorkProtectedField = value;
                }
            }

            /// <remarks/>
            public string ActiveDirectoryGUID
            {
                get
                {
                    return activeDirectoryGUIDField;
                }
                set
                {
                    activeDirectoryGUIDField = value;
                }
            }

            /// <remarks/>
            public DateTime CreationDate
            {
                get
                {
                    return creationDateField;
                }
                set
                {
                    creationDateField = value;
                }
            }

            /// <remarks/>
            [XmlElement("ExtendedAttribute")]
            public ProjectResourceExtendedAttribute[] ExtendedAttribute
            {
                get
                {
                    return extendedAttributeField;
                }
                set
                {
                    extendedAttributeField = value;
                }
            }

            /// <remarks/>
            [XmlElement("Baseline")]
            public ProjectResourceBaseline[] Baseline
            {
                get
                {
                    return baselineField;
                }
                set
                {
                    baselineField = value;
                }
            }

            /// <remarks/>
            [XmlElement("OutlineCode")]
            public ProjectResourceOutlineCode[] OutlineCode
            {
                get
                {
                    return outlineCodeField;
                }
                set
                {
                    outlineCodeField = value;
                }
            }

            /// <remarks/>
            public bool IsCostResource
            {
                get
                {
                    return isCostResourceField;
                }
                set
                {
                    isCostResourceField = value;
                }
            }

            /// <remarks/>
            public string AssnOwner
            {
                get
                {
                    return assnOwnerField;
                }
                set
                {
                    assnOwnerField = value;
                }
            }

            /// <remarks/>
            public string AssnOwnerGuid
            {
                get
                {
                    return assnOwnerGuidField;
                }
                set
                {
                    assnOwnerGuidField = value;
                }
            }

            /// <remarks/>
            public bool IsBudget
            {
                get
                {
                    return isBudgetField;
                }
                set
                {
                    isBudgetField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("AvailabilityPeriod", IsNullable = false)]
            public ProjectResourceAvailabilityPeriod[] AvailabilityPeriods
            {
                get
                {
                    return availabilityPeriodsField;
                }
                set
                {
                    availabilityPeriodsField = value;
                }
            }

            /// <remarks/>
            [XmlArrayItem("Rate", IsNullable = false)]
            public ProjectResourceRate[] Rates
            {
                get
                {
                    return ratesField;
                }
                set
                {
                    ratesField = value;
                }
            }

            /// <remarks/>
            [XmlElement("TimephasedData")]
            public ProjectResourceTimephasedData[] TimephasedData
            {
                get
                {
                    return timephasedDataField;
                }
                set
                {
                    timephasedDataField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResourceExtendedAttribute
        {

            private string fieldIDField;

            private string valueField;

            private string valueGUIDField;

            private byte durationFormatField;

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueGUID
            {
                get
                {
                    return valueGUIDField;
                }
                set
                {
                    valueGUIDField = value;
                }
            }

            /// <remarks/>
            public byte DurationFormat
            {
                get
                {
                    return durationFormatField;
                }
                set
                {
                    durationFormatField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResourceBaseline
        {

            private string numberField;

            private string workField;

            private float costField;

            private float bCWSField;

            private float bCWPField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Number
            {
                get
                {
                    return numberField;
                }
                set
                {
                    numberField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Work
            {
                get
                {
                    return workField;
                }
                set
                {
                    workField = value;
                }
            }

            /// <remarks/>
            public float Cost
            {
                get
                {
                    return costField;
                }
                set
                {
                    costField = value;
                }
            }

            /// <remarks/>
            public float BCWS
            {
                get
                {
                    return bCWSField;
                }
                set
                {
                    bCWSField = value;
                }
            }

            /// <remarks/>
            public float BCWP
            {
                get
                {
                    return bCWPField;
                }
                set
                {
                    bCWPField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResourceOutlineCode
        {

            private string fieldIDField;

            private string valueIDField;

            private string valueGUIDField;

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueID
            {
                get
                {
                    return valueIDField;
                }
                set
                {
                    valueIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueGUID
            {
                get
                {
                    return valueGUIDField;
                }
                set
                {
                    valueGUIDField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResourceAvailabilityPeriod
        {

            private DateTime availableFromField;

            private DateTime availableToField;

            private float availableUnitsField;

            /// <remarks/>
            public DateTime AvailableFrom
            {
                get
                {
                    return availableFromField;
                }
                set
                {
                    availableFromField = value;
                }
            }

            /// <remarks/>
            public DateTime AvailableTo
            {
                get
                {
                    return availableToField;
                }
                set
                {
                    availableToField = value;
                }
            }

            /// <remarks/>
            public float AvailableUnits
            {
                get
                {
                    return availableUnitsField;
                }
                set
                {
                    availableUnitsField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResourceRate
        {

            private DateTime ratesFromField;

            private DateTime ratesToField;

            private byte rateTableField;

            private decimal standardRateField;

            private byte standardRateFormatField;

            private decimal overtimeRateField;

            private byte overtimeRateFormatField;

            private decimal costPerUseField;

            /// <remarks/>
            public DateTime RatesFrom
            {
                get
                {
                    return ratesFromField;
                }
                set
                {
                    ratesFromField = value;
                }
            }

            /// <remarks/>
            public DateTime RatesTo
            {
                get
                {
                    return ratesToField;
                }
                set
                {
                    ratesToField = value;
                }
            }

            /// <remarks/>
            public byte RateTable
            {
                get
                {
                    return rateTableField;
                }
                set
                {
                    rateTableField = value;
                }
            }

            /// <remarks/>
            public decimal StandardRate
            {
                get
                {
                    return standardRateField;
                }
                set
                {
                    standardRateField = value;
                }
            }

            /// <remarks/>
            public byte StandardRateFormat
            {
                get
                {
                    return standardRateFormatField;
                }
                set
                {
                    standardRateFormatField = value;
                }
            }

            /// <remarks/>
            public decimal OvertimeRate
            {
                get
                {
                    return overtimeRateField;
                }
                set
                {
                    overtimeRateField = value;
                }
            }

            /// <remarks/>
            public byte OvertimeRateFormat
            {
                get
                {
                    return overtimeRateFormatField;
                }
                set
                {
                    overtimeRateFormatField = value;
                }
            }

            /// <remarks/>
            public decimal CostPerUse
            {
                get
                {
                    return costPerUseField;
                }
                set
                {
                    costPerUseField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectResourceTimephasedData
        {

            private byte typeField;

            private string uIDField;

            private DateTime startField;

            private DateTime finishField;

            private byte unitField;

            private string valueField;

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            public byte Unit
            {
                get
                {
                    return unitField;
                }
                set
                {
                    unitField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectAssignment
        {

            private string uIDField;

            private string taskUIDField;

            private string resourceUIDField;

            private string percentWorkCompleteField;

            private string actualCostField;

            private DateTime actualFinishField;

            private string actualOvertimeCostField;

            private string actualOvertimeWorkField;

            private DateTime actualStartField;

            private string actualWorkField;

            private float aCWPField;

            private bool confirmedField;

            private string costField;

            private byte costRateTableField;

            private float costVarianceField;

            private float cvField;

            private string delayField;

            private DateTime finishField;

            private string finishVarianceField;

            private string hyperlinkField;

            private string hyperlinkAddressField;

            private string hyperlinkSubAddressField;

            private float workVarianceField;

            private bool hasFixedRateUnitsField;

            private bool fixedMaterialField;

            private string levelingDelayField;

            private byte levelingDelayFormatField;

            private bool linkedFieldsField;

            private bool milestoneField;

            private string notesField;

            private bool overallocatedField;

            private string overtimeCostField;

            private string overtimeWorkField;

            private float peakUnitsField;

            private string regularWorkField;

            private string remainingCostField;

            private string remainingOvertimeCostField;

            private string remainingOvertimeWorkField;

            private string remainingWorkField;

            private bool responsePendingField;

            private DateTime startField;

            private DateTime stopField;

            private DateTime resumeField;

            private string startVarianceField;

            private bool summaryField;

            private float svField;

            private float unitsField;

            private bool updateNeededField;

            private float vACField;

            private string workField;

            private byte workContourField;

            private float bCWSField;

            private float bCWPField;

            private byte bookingTypeField;

            private string actualWorkProtectedField;

            private string actualOvertimeWorkProtectedField;

            private DateTime creationDateField;

            private string assnOwnerField;

            private string assnOwnerGuidField;

            private string budgetCostField;

            private string budgetWorkField;

            private ProjectAssignmentExtendedAttribute[] extendedAttributeField;

            private ProjectAssignmentBaseline[] baselineField;

            private ProjectAssignmentTimephasedData[] timephasedDataField;

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string TaskUID
            {
                get
                {
                    return taskUIDField;
                }
                set
                {
                    taskUIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ResourceUID
            {
                get
                {
                    return resourceUIDField;
                }
                set
                {
                    resourceUIDField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string PercentWorkComplete
            {
                get
                {
                    return percentWorkCompleteField;
                }
                set
                {
                    percentWorkCompleteField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ActualCost
            {
                get
                {
                    return actualCostField;
                }
                set
                {
                    actualCostField = value;
                }
            }

            /// <remarks/>
            public DateTime ActualFinish
            {
                get
                {
                    return actualFinishField;
                }
                set
                {
                    actualFinishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ActualOvertimeCost
            {
                get
                {
                    return actualOvertimeCostField;
                }
                set
                {
                    actualOvertimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualOvertimeWork
            {
                get
                {
                    return actualOvertimeWorkField;
                }
                set
                {
                    actualOvertimeWorkField = value;
                }
            }

            /// <remarks/>
            public DateTime ActualStart
            {
                get
                {
                    return actualStartField;
                }
                set
                {
                    actualStartField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualWork
            {
                get
                {
                    return actualWorkField;
                }
                set
                {
                    actualWorkField = value;
                }
            }

            /// <remarks/>
            public float ACWP
            {
                get
                {
                    return aCWPField;
                }
                set
                {
                    aCWPField = value;
                }
            }

            /// <remarks/>
            public bool Confirmed
            {
                get
                {
                    return confirmedField;
                }
                set
                {
                    confirmedField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Cost
            {
                get
                {
                    return costField;
                }
                set
                {
                    costField = value;
                }
            }

            /// <remarks/>
            public byte CostRateTable
            {
                get
                {
                    return costRateTableField;
                }
                set
                {
                    costRateTableField = value;
                }
            }

            /// <remarks/>
            public float CostVariance
            {
                get
                {
                    return costVarianceField;
                }
                set
                {
                    costVarianceField = value;
                }
            }

            /// <remarks/>
            public float CV
            {
                get
                {
                    return cvField;
                }
                set
                {
                    cvField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string Delay
            {
                get
                {
                    return delayField;
                }
                set
                {
                    delayField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string FinishVariance
            {
                get
                {
                    return finishVarianceField;
                }
                set
                {
                    finishVarianceField = value;
                }
            }

            /// <remarks/>
            public string Hyperlink
            {
                get
                {
                    return hyperlinkField;
                }
                set
                {
                    hyperlinkField = value;
                }
            }

            /// <remarks/>
            public string HyperlinkAddress
            {
                get
                {
                    return hyperlinkAddressField;
                }
                set
                {
                    hyperlinkAddressField = value;
                }
            }

            /// <remarks/>
            public string HyperlinkSubAddress
            {
                get
                {
                    return hyperlinkSubAddressField;
                }
                set
                {
                    hyperlinkSubAddressField = value;
                }
            }

            /// <remarks/>
            public float WorkVariance
            {
                get
                {
                    return workVarianceField;
                }
                set
                {
                    workVarianceField = value;
                }
            }

            /// <remarks/>
            public bool HasFixedRateUnits
            {
                get
                {
                    return hasFixedRateUnitsField;
                }
                set
                {
                    hasFixedRateUnitsField = value;
                }
            }

            /// <remarks/>
            public bool FixedMaterial
            {
                get
                {
                    return fixedMaterialField;
                }
                set
                {
                    fixedMaterialField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string LevelingDelay
            {
                get
                {
                    return levelingDelayField;
                }
                set
                {
                    levelingDelayField = value;
                }
            }

            /// <remarks/>
            public byte LevelingDelayFormat
            {
                get
                {
                    return levelingDelayFormatField;
                }
                set
                {
                    levelingDelayFormatField = value;
                }
            }

            /// <remarks/>
            public bool LinkedFields
            {
                get
                {
                    return linkedFieldsField;
                }
                set
                {
                    linkedFieldsField = value;
                }
            }

            /// <remarks/>
            public bool Milestone
            {
                get
                {
                    return milestoneField;
                }
                set
                {
                    milestoneField = value;
                }
            }

            /// <remarks/>
            public string Notes
            {
                get
                {
                    return notesField;
                }
                set
                {
                    notesField = value;
                }
            }

            /// <remarks/>
            public bool Overallocated
            {
                get
                {
                    return overallocatedField;
                }
                set
                {
                    overallocatedField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string OvertimeCost
            {
                get
                {
                    return overtimeCostField;
                }
                set
                {
                    overtimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string OvertimeWork
            {
                get
                {
                    return overtimeWorkField;
                }
                set
                {
                    overtimeWorkField = value;
                }
            }

            /// <remarks/>
            public float PeakUnits
            {
                get
                {
                    return peakUnitsField;
                }
                set
                {
                    peakUnitsField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RegularWork
            {
                get
                {
                    return regularWorkField;
                }
                set
                {
                    regularWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string RemainingCost
            {
                get
                {
                    return remainingCostField;
                }
                set
                {
                    remainingCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string RemainingOvertimeCost
            {
                get
                {
                    return remainingOvertimeCostField;
                }
                set
                {
                    remainingOvertimeCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingOvertimeWork
            {
                get
                {
                    return remainingOvertimeWorkField;
                }
                set
                {
                    remainingOvertimeWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string RemainingWork
            {
                get
                {
                    return remainingWorkField;
                }
                set
                {
                    remainingWorkField = value;
                }
            }

            /// <remarks/>
            public bool ResponsePending
            {
                get
                {
                    return responsePendingField;
                }
                set
                {
                    responsePendingField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Stop
            {
                get
                {
                    return stopField;
                }
                set
                {
                    stopField = value;
                }
            }

            /// <remarks/>
            public DateTime Resume
            {
                get
                {
                    return resumeField;
                }
                set
                {
                    resumeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string StartVariance
            {
                get
                {
                    return startVarianceField;
                }
                set
                {
                    startVarianceField = value;
                }
            }

            /// <remarks/>
            public bool Summary
            {
                get
                {
                    return summaryField;
                }
                set
                {
                    summaryField = value;
                }
            }

            /// <remarks/>
            public float SV
            {
                get
                {
                    return svField;
                }
                set
                {
                    svField = value;
                }
            }

            /// <remarks/>
            public float Units
            {
                get
                {
                    return unitsField;
                }
                set
                {
                    unitsField = value;
                }
            }

            /// <remarks/>
            public bool UpdateNeeded
            {
                get
                {
                    return updateNeededField;
                }
                set
                {
                    updateNeededField = value;
                }
            }

            /// <remarks/>
            public float VAC
            {
                get
                {
                    return vACField;
                }
                set
                {
                    vACField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Work
            {
                get
                {
                    return workField;
                }
                set
                {
                    workField = value;
                }
            }

            /// <remarks/>
            public byte WorkContour
            {
                get
                {
                    return workContourField;
                }
                set
                {
                    workContourField = value;
                }
            }

            /// <remarks/>
            public float BCWS
            {
                get
                {
                    return bCWSField;
                }
                set
                {
                    bCWSField = value;
                }
            }

            /// <remarks/>
            public float BCWP
            {
                get
                {
                    return bCWPField;
                }
                set
                {
                    bCWPField = value;
                }
            }

            /// <remarks/>
            public byte BookingType
            {
                get
                {
                    return bookingTypeField;
                }
                set
                {
                    bookingTypeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualWorkProtected
            {
                get
                {
                    return actualWorkProtectedField;
                }
                set
                {
                    actualWorkProtectedField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string ActualOvertimeWorkProtected
            {
                get
                {
                    return actualOvertimeWorkProtectedField;
                }
                set
                {
                    actualOvertimeWorkProtectedField = value;
                }
            }

            /// <remarks/>
            public DateTime CreationDate
            {
                get
                {
                    return creationDateField;
                }
                set
                {
                    creationDateField = value;
                }
            }

            /// <remarks/>
            public string AssnOwner
            {
                get
                {
                    return assnOwnerField;
                }
                set
                {
                    assnOwnerField = value;
                }
            }

            /// <remarks/>
            public string AssnOwnerGuid
            {
                get
                {
                    return assnOwnerGuidField;
                }
                set
                {
                    assnOwnerGuidField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string BudgetCost
            {
                get
                {
                    return budgetCostField;
                }
                set
                {
                    budgetCostField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string BudgetWork
            {
                get
                {
                    return budgetWorkField;
                }
                set
                {
                    budgetWorkField = value;
                }
            }

            /// <remarks/>
            [XmlElement("ExtendedAttribute")]
            public ProjectAssignmentExtendedAttribute[] ExtendedAttribute
            {
                get
                {
                    return extendedAttributeField;
                }
                set
                {
                    extendedAttributeField = value;
                }
            }

            /// <remarks/>
            [XmlElement("Baseline")]
            public ProjectAssignmentBaseline[] Baseline
            {
                get
                {
                    return baselineField;
                }
                set
                {
                    baselineField = value;
                }
            }

            /// <remarks/>
            [XmlElement("TimephasedData")]
            public ProjectAssignmentTimephasedData[] TimephasedData
            {
                get
                {
                    return timephasedDataField;
                }
                set
                {
                    timephasedDataField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectAssignmentExtendedAttribute
        {

            private string fieldIDField;

            private string valueField;

            private string valueGUIDField;

            private byte durationFormatField;

            /// <remarks/>
            public string FieldID
            {
                get
                {
                    return fieldIDField;
                }
                set
                {
                    fieldIDField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string ValueGUID
            {
                get
                {
                    return valueGUIDField;
                }
                set
                {
                    valueGUIDField = value;
                }
            }

            /// <remarks/>
            public byte DurationFormat
            {
                get
                {
                    return durationFormatField;
                }
                set
                {
                    durationFormatField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectAssignmentBaseline
        {

            private ProjectAssignmentBaselineTimephasedData[] timephasedDataField;

            private string numberField;

            private string startField;

            private string finishField;

            private string workField;

            private string costField;

            private float bCWSField;

            private float bCWPField;

            /// <remarks/>
            [XmlElement("TimephasedData")]
            public ProjectAssignmentBaselineTimephasedData[] TimephasedData
            {
                get
                {
                    return timephasedDataField;
                }
                set
                {
                    timephasedDataField = value;
                }
            }

            /// <remarks/>
            public string Number
            {
                get
                {
                    return numberField;
                }
                set
                {
                    numberField = value;
                }
            }

            /// <remarks/>
            public string Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public string Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "duration")]
            public string Work
            {
                get
                {
                    return workField;
                }
                set
                {
                    workField = value;
                }
            }

            /// <remarks/>
            public string Cost
            {
                get
                {
                    return costField;
                }
                set
                {
                    costField = value;
                }
            }

            /// <remarks/>
            public float BCWS
            {
                get
                {
                    return bCWSField;
                }
                set
                {
                    bCWSField = value;
                }
            }

            /// <remarks/>
            public float BCWP
            {
                get
                {
                    return bCWPField;
                }
                set
                {
                    bCWPField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectAssignmentBaselineTimephasedData
        {

            private byte typeField;

            private string uIDField;

            private DateTime startField;

            private DateTime finishField;

            private byte unitField;

            private string valueField;

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            public byte Unit
            {
                get
                {
                    return unitField;
                }
                set
                {
                    unitField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/project")]
        public partial class ProjectAssignmentTimephasedData
        {

            private byte typeField;

            private string uIDField;

            private DateTime startField;

            private DateTime finishField;

            private byte unitField;

            private string valueField;

            /// <remarks/>
            public byte Type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            [XmlElement(DataType = "integer")]
            public string UID
            {
                get
                {
                    return uIDField;
                }
                set
                {
                    uIDField = value;
                }
            }

            /// <remarks/>
            public DateTime Start
            {
                get
                {
                    return startField;
                }
                set
                {
                    startField = value;
                }
            }

            /// <remarks/>
            public DateTime Finish
            {
                get
                {
                    return finishField;
                }
                set
                {
                    finishField = value;
                }
            }

            /// <remarks/>
            public byte Unit
            {
                get
                {
                    return unitField;
                }
                set
                {
                    unitField = value;
                }
            }

            /// <remarks/>
            public string Value
            {
                get
                {
                    return valueField;
                }
                set
                {
                    valueField = value;
                }
            }
        }


    }
}
