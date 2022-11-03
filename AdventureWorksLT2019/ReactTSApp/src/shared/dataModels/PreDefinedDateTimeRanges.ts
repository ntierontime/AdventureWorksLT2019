import moment from "moment";

export enum PreDefinedDateTimeRanges {
    AllTime = 'AllTime',
    Custom = 'Custom',
    LastTenYears = 'LastTenYears',
    LastFiveYears = 'LastFiveYears',
    LastYear = 'LastYear',
    LastSixMonths = 'LastSixMonths',
    LastThreeMonths = 'LastThreeMonths',
    LastMonth = 'LastMonth',
    LastWeek = 'LastWeek',
    Yesterday = 'Yesterday',
    LastHour = 'LastHour',
    ThisYear = 'ThisYear',
    ThisMonth = 'ThisMonth',
    ThisWeek = 'ThisWeek',
    Today = 'Today',
    ThisHour = 'ThisHour',
    NextHour = 'NextHour',
    Tomorrow = 'Tomorrow',
    NextWeek = 'NextWeek',
    NextMonth = 'NextMonth',
    NextThreeMonths = 'NextThreeMonths',
    NextSixMonths = 'NextSixMonths',
    NextYear = 'NextYear',
    NextFiveYears = 'NextFiveYears',
    NextTenYears = 'NextTenYears',
}

export function getDateRange(referenceDate: Date, type: PreDefinedDateTimeRanges): {lowerBound: Date, upperBound: Date} | null {
    var lowerBound = moment(referenceDate);
    var upperBound = moment(referenceDate);
    switch (type) {
        case PreDefinedDateTimeRanges.LastTenYears:
            return { lowerBound: lowerBound.subtract(10, "years").startOf("years").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.LastFiveYears:
            return { lowerBound: lowerBound.subtract(5, "years").startOf("years").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.LastYear:
            return { lowerBound: lowerBound.subtract(1, "years").startOf("years").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.LastSixMonths:
            return { lowerBound: lowerBound.subtract(6, "months").startOf("months").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.LastThreeMonths:
            return { lowerBound: lowerBound.subtract(3, "months").startOf("months").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.LastMonth:
            return { lowerBound: lowerBound.subtract(1, "months").startOf("months").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.LastWeek:
            return { lowerBound: lowerBound.subtract(1, "weeks").startOf("weeks").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.Yesterday:
            return { lowerBound: lowerBound.subtract(1, "days").startOf("days").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.ThisYear:
            return { lowerBound: lowerBound.startOf("years").toDate(), upperBound: upperBound.endOf("years").toDate() };
        case PreDefinedDateTimeRanges.ThisMonth:
            return { lowerBound: lowerBound.startOf("months").toDate(), upperBound: upperBound.endOf("months").toDate() };
        case PreDefinedDateTimeRanges.ThisWeek:
            return { lowerBound: lowerBound.startOf("weeks").toDate(), upperBound: upperBound.endOf("weeks").toDate() };
        case PreDefinedDateTimeRanges.Today:
            return { lowerBound: lowerBound.startOf("days").toDate(), upperBound: upperBound.endOf("days").toDate() };
        case PreDefinedDateTimeRanges.Tomorrow:
            return { lowerBound: lowerBound.add(1, "days").startOf("days").toDate(), upperBound: upperBound.add(1, "days").endOf("days").toDate() };
        case PreDefinedDateTimeRanges.NextWeek:
            return { lowerBound: lowerBound.add(1, "weeks").startOf("weeks").toDate(), upperBound: upperBound.add(1, "weeks").endOf("weeks").toDate() };
        case PreDefinedDateTimeRanges.NextMonth:
            return { lowerBound: lowerBound.add(1, "months").startOf("months").toDate(), upperBound: upperBound.add(1, "months").endOf("months").toDate() };
        case PreDefinedDateTimeRanges.NextThreeMonths:
            return { lowerBound: lowerBound.toDate(), upperBound: upperBound.add(3, "months").endOf("months").toDate() };
        case PreDefinedDateTimeRanges.NextSixMonths:
            return { lowerBound: lowerBound.toDate(), upperBound: upperBound.add(6, "months").endOf("months").toDate() };
        case PreDefinedDateTimeRanges.NextYear:
            return { lowerBound: lowerBound.toDate(), upperBound: upperBound.add(1, "years").endOf("years").toDate() };
        case PreDefinedDateTimeRanges.NextFiveYears:
            return { lowerBound: lowerBound.toDate(), upperBound: upperBound.add(5, "years").endOf("years").toDate() };
        case PreDefinedDateTimeRanges.NextTenYears:
            return { lowerBound: lowerBound.toDate(), upperBound: upperBound.add(10, "years").endOf("years").toDate() };
        case PreDefinedDateTimeRanges.Custom:
            return { lowerBound: lowerBound.subtract(10, "years").startOf("years").toDate(), upperBound: upperBound.toDate() };
        case PreDefinedDateTimeRanges.AllTime:
            return { lowerBound: null, upperBound: null };
    }
            
    return { lowerBound: null, upperBound: null };
}