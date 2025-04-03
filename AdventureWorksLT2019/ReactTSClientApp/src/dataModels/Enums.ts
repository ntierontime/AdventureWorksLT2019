// please use enum instead of integer or string values directly in your code

export enum AddressTypeOptions
{
    Billing = 'Billing',
    Home = 'Home',
    Main_Office = 'Main_Office',
    Primary = 'Primary',
    Shipping = 'Shipping',
    Archive = 'Archive',
}


export enum AspNetRolesOptions
{
    SystemAdmin = 'SystemAdmin',
    Consumer = 'Consumer',
    Owner = 'Owner',
    Employee = 'Employee',
    Visitor = 'Visitor',
    BasicUser = 'BasicUser',
}


export enum AttendanceStatusOptions
{
    Pending = 'Pending',
    InProgress = 'InProgress',
    Attended = 'Attended',
    Absent = 'Absent',
    Cancelled = 'Cancelled',
    Rescheduled = 'Rescheduled',
}


export enum ContactTypeOptions
{
    Emergency = 'Emergency',
    Assistant_Sales_Agent = 'Assistant_Sales_Agent',
    Assistant_Sales_Representative = 'Assistant_Sales_Representative',
    Coordinator_Foreign_Markets = 'Coordinator_Foreign_Markets',
    Export_Administrator = 'Export_Administrator',
    International_Marketing_Manager = 'International_Marketing_Manager',
    Marketing_Assistant = 'Marketing_Assistant',
    Marketing_Manager = 'Marketing_Manager',
    Marketing_Representative = 'Marketing_Representative',
    Order_Administrator = 'Order_Administrator',
    Owner = 'Owner',
    Owner_Marketing_Assistant = 'Owner_Marketing_Assistant',
    Product_Manager = 'Product_Manager',
    Purchasing_Agent = 'Purchasing_Agent',
    Purchasing_Manager = 'Purchasing_Manager',
    Regional_Account_Representative = 'Regional_Account_Representative',
    Sales_Agent = 'Sales_Agent',
    Sales_Associate = 'Sales_Associate',
    Sales_Manager = 'Sales_Manager',
    Sales_Representative = 'Sales_Representative',
    Accounting_Manager = 'Accounting_Manager',
}


export enum CourseStructureTypeOptions
{
    OneLesson = 'OneLesson',
    OneSection = 'OneSection',
    MultipleSection = 'MultipleSection',
}


export enum CultureOptions
{
    Arabic = 'ar    ',
    English = 'en    ',
    Spanish = 'es    ',
    French = 'fr    ',
    Hebrew = 'he    ',
    Thai = 'th    ',
    Chinese = 'zh-cht',
}


export enum DayOfWeekCodeListOptions
{
    Sunday = 'Sunday',
    Monday = 'Monday',
    Tuesday = 'Tuesday',
    Wednesday = 'Wednesday',
    Thursday = 'Thursday',
    Friday = 'Friday',
    Saturday = 'Saturday',
    AllDays = 'AllDays',
    Holiday = 'Holiday',
}


export enum EmailTypeOptions
{
    Personal = 'Personal',
    Work = 'Work',
}


export enum EntityStatusCodeOptions
{
    Created = 'Created',
    Active = 'Active',
    ToBeDeleted = 'ToBeDeleted',
    Disabled = 'Disabled',
}


export enum EventStatusOptions
{
    Created = 'Created',
    OpenToBook = 'OpenToBook',
    Rescheduled = 'Rescheduled',
    Pending = 'Pending',
    InProgress = 'InProgress',
    Completed = 'Completed',
    Cancelled = 'Cancelled',
    PartialCancelled = 'PartialCancelled',
}


export enum GenderOptions
{
    Male = 'Male',
    Female = 'Female',
    NonBinary = 'NonBinary',
    Custom = 'Custom',
}


export enum ItemPriceModelOptions
{
    BundleOriginalPricing = 'BundleOriginalPricing',
    StandardPricing = 'StandardPricing',
    PackagePricing = 'PackagePricing',
    GraduatedPricing = 'GraduatedPricing',
    VolumnPricing = 'VolumnPricing',
    CustomerChoose = 'CustomerChoose',
}


export enum ItemStatusCodeOptions
{
    Created = 'Created',
    Active = 'Active',
    Discontinued = 'Discontinued',
}


export enum ItemTaxIncludeTypeOptions
{
    NotIncludeTax = 'NotIncludeTax',
    IncludeTax = 'IncludeTax',
    AutoCalculated = 'AutoCalculated',
}


export enum ItemTypeOptions
{
    DigitalProduct = 'DigitalProduct',
    PhysicalProduct = 'PhysicalProduct',
    DigitalService = 'DigitalService',
    Service = 'Service',
    Bundle = 'Bundle',
    Subscription = 'Subscription',
}


export enum LessonTypeOptions
{
    Lesson = 'Lesson',
    Lecture = 'Lecture',
    Webinar = 'Webinar',
}


export enum NotificationStatusOptions
{
    Pending = 'Pending',
    Read = 'Read',
    Executed = 'Executed',
}


export enum NotificationTypeOptions
{
    LaunchWizard = 'LaunchWizard',
}


export enum OrderStatusCodeOptions
{
    Ordered = 'Ordered',
    Processing = 'Processing',
    Unpaid = 'Unpaid',
    PaymentFailed = 'PaymentFailed',
    PartiallyShipped = 'PartiallyShipped',
    PartiallyDelivered = 'PartiallyDelivered',
    Shipped = 'Shipped',
    Delivered = 'Delivered',
    PartiallyReturns = 'PartiallyReturns',
    Returns = 'Returns',
    Cancelled = 'Cancelled',
    ReadyForFeedback = 'ReadyForFeedback',
}


export enum PaymentGatewayOptions
{
    NDollarGateWay = 'NDollarGateWay',
    SandBox = 'SandBox',
    Stripe = 'Stripe',
}


export enum PaymentStatusOptions
{
    Pending = 'Pending',
    Complete = 'Complete',
    Refunded = 'Refunded',
    Failed = 'Failed',
    Abandoned = 'Abandoned',
    Revoked = 'Revoked',
    Preapproved = 'Preapproved',
    OnHold = 'OnHold',
    Cancelled = 'Cancelled',
    Subscription = 'Subscription',
    Other = 'Other',
}


export enum PersonalRelationCodeOptions
{
    Parent = 'Parent',
    Spouse = 'Spouse',
    Child = 'Child',
    Sibling = 'Sibling',
    Pet = 'Pet',
    Friend = 'Friend',
}


export enum PhoneNumberTypeOptions
{
    Cell = 'Cell',
    Home = 'Home',
    Work = 'Work',
}


export enum RecursiveScheduleTypeOptions
{
    OneTime = 'OneTime',
    Daily = 'Daily',
    Weekly = 'Weekly',
    Monthly = 'Monthly',
    Annually = 'Annually',
}


export enum RegistrationStatusOptions
{
    Registered = 'Registered',
    InProgress = 'InProgress',
    Completed = 'Completed',
    Cancelled = 'Cancelled',
    PartialCancelled = 'PartialCancelled',
    Revoked = 'Revoked',
}


export enum ScheduleOptionOptions
{
    Immediate = 'Immediate',
    Scheduled = 'Scheduled',
    Subscription = 'Subscription',
}


export enum ShippingMethodOptions
{
    Electronic = 'Electronic',
    Pickup = 'Pickup',
    Delivery = 'Delivery',
}


export enum ShippingPickupOptionOptions
{
    None = 'None',
    CurbsidePickup = 'CurbsidePickup',
    BuyOnlinePickupInStore = 'BuyOnlinePickupInStore',
    LockerPickerup = 'LockerPickerup',
}


export enum ShippingTimeFrameOptions
{
    StandardShipping = 'StandardShipping',
    ExpeditedShipping = 'ExpeditedShipping',
    SameDayDelivery = 'SameDayDelivery',
    OvernightDelivery = 'OvernightDelivery',
    ExpressDelivery = 'ExpressDelivery',
}


export enum SkillLevelOptions
{
    Beginner = 'Beginner',
    Intermediate = 'Intermediate',
    Advanced = 'Advanced',
    Expert = 'Expert',
}


export enum SpecialOfferDurationOptions
{
    Forever = 'Forever',
    Once = 'Once',
    DateTimeRange = 'DateTimeRange',
}


export enum SubscriberPlanItemTypeOptions
{
    Transaction = 'Transaction',
    Calendar = 'Calendar',
}


export enum SubscriberPlanTypeOptions
{
    Free = 'Free',
    Recursive = 'Recursive',
    Custom = 'Custom',
}


export enum SubscriberTypeOptions
{
    Partner = 'Partner',
    Consumer = 'Consumer',
}


export enum TaxRateTypeOptions
{
    SalesTax = 'SalesTax',
    VAT = 'VAT',
    GST = 'GST',
    CustomTax = 'CustomTax',
}


export enum TimeOffDateOptionOptions
{
    VacationTime = 'VacationTime',
    PersonalDays = 'PersonalDays',
    SickDays = 'SickDays',
    BereavementLeave = 'BereavementLeave',
    JuryDury = 'JuryDury',
    PublicHoliday = 'PublicHoliday',
    MaturnityLeave = 'MaturnityLeave',
    ParentalLeave = 'ParentalLeave',
    CompassionateCareLeave = 'CompassionateCareLeave',
    PersonalLeave = 'PersonalLeave',
    MilitaryLeave = 'MilitaryLeave',
    ValunteerLeave = 'ValunteerLeave',
}


export enum TransactionDirectionOptions
{
    PaymentFromConsumer = 'PaymentFromConsumer',
    PaymentToPartner = 'PaymentToPartner',
    PaymentToSystem = 'PaymentToSystem',
    RefundFromPartner = 'RefundFromPartner',
    RefundToConsumer = 'RefundToConsumer',
    RefundFromSystem = 'RefundFromSystem',
    RewardPointConversion = 'RewardPointConversion',
}


export enum TransactionStatusOptions
{
    Pending = 'Pending',
    Complete = 'Complete',
    Failed = 'Failed',
}


export enum TransactionTypeOptions
{
    Payment = 'Payment',
    Refund = 'Refund',
    Reward = 'Reward',
}


export enum VisibilityOptions
{
    Private = 'Private',
    Viewable = 'Viewable',
    Searchable = 'Searchable',
}


