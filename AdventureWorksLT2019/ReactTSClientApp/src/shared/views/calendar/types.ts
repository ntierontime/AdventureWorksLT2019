import dayjs from "dayjs";

export enum WeekDays {
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    AllDays = 7,
}

export interface WeeklyOfficeHoursSetting {
    // // will copy startTime-endTime and lunchHourStartTime/lunchHourEndTime to all weekdays(Sunday to Saturday), not in database
    // sameScheduleAllDays?: boolean;

    // the following are settings in database
    workInHoliday?: boolean;
    haveLunchHour: boolean;
    weekDayOfficeHours: WeekDayOfficeHour[];
}

export interface WeekDayOfficeHour {
    weekDay: WeekDays;
    enabled: boolean;
    label: string;
    startTime: any;
    endTime: any;
    lunchHourStartTime: any;
    lunchHourEndTime: any;
}

export interface CalendarDateStatus {
    dateTime: dayjs.Dayjs;
    disabled: boolean;
}

export interface CalendarTimeSlot {
    startDateTime: dayjs.Dayjs;
    endDateTime: dayjs.Dayjs;
    section: string;
}

export interface BookedTimeSlot {
    startDateTime: dayjs.Dayjs;
    endDateTime: dayjs.Dayjs;
    duration: number; // in minutes
    bookedByID: number;
    bookedByName: string;
}

export const inCalendarTimeSlot = (calendarTimeSlot: CalendarTimeSlot, bookedTimeSlot: BookedTimeSlot) : boolean => {
    const result = 
        !!bookedTimeSlot && !!calendarTimeSlot && !!calendarTimeSlot.startDateTime && !!calendarTimeSlot.endDateTime;
    const result1 = 
        result &&
        (
            (calendarTimeSlot.startDateTime.isAfter(bookedTimeSlot.startDateTime) && calendarTimeSlot.startDateTime.isBefore(bookedTimeSlot.endDateTime)) ||
            (calendarTimeSlot.endDateTime.isAfter(bookedTimeSlot.startDateTime) && calendarTimeSlot.endDateTime.isBefore(bookedTimeSlot.endDateTime))

        );
    // console.log("A-inCalendarTimeSlot", result, result1, calendarTimeSlot?.startDateTime?.toLocaleString(), calendarTimeSlot?.endDateTime?.toLocaleString());
    // console.log("B-inCalendarTimeSlot", result, result1, bookedTimeSlot?.startDateTime?.toLocaleString(), bookedTimeSlot?.endDateTime?.toLocaleString());
    return result1;
}