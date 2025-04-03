import React from "react";
import { Grid, Typography } from "@mui/material";
import _ from "lodash";

import dayjs from "dayjs";
import { useTranslation } from "react-i18next";
import { BookedTimeSlot, CalendarTimeSlot, inCalendarTimeSlot } from "./types";
import { TimeSlot } from "./TimeSlot";
import { xs2sm3md4lg6xl6GridItem } from "../ResponsiveGridItem";

// fullyBookedOrPrempty logic will be handled in upper level
export interface WeeklyCalendarTimeBookingProps {
    requiredDuration: number;
    bookedByThisUserTimeSlots?: BookedTimeSlot[];
    bookedTimeSlots?: BookedTimeSlot[];
    startTime: number; // in minutes
    endTime: number; // in minutes
    interval: number; // in minutes
    selectedDay?: string;
    selectedTime?: string;
    setSelectedTime: (newValue: string) => void;
}

export const WeeklyCalendarTimeBooking = (props: WeeklyCalendarTimeBookingProps): JSX.Element => {
    const { t } = useTranslation();

    const { requiredDuration, bookedByThisUserTimeSlots, bookedTimeSlots, startTime, endTime, interval, selectedDay, selectedTime, setSelectedTime } = props;

    const startTimeMoment = dayjs(selectedDay).startOf('day').add(startTime, 'minute');
    const endTimeMoment = dayjs(selectedDay).startOf('day').add(endTime, 'minute');
    const totalWorkingDuration = endTimeMoment.diff(startTimeMoment, 'minute');
    const totalWorkingSlot = Math.ceil(totalWorkingDuration / interval);

    const slotsOfThisDay = [...Array(totalWorkingSlot)].map((_, i) => {
        const thisSlotStartTime = dayjs(dayjs(startTimeMoment.toISOString()).add(i * interval, "minutes"));
        const thisSlotEndTime = dayjs(dayjs(startTimeMoment.toISOString()).add((i + 1)* interval, "minutes"));
        const section = thisSlotStartTime.hour() < 12
            ? "Morning"
            : thisSlotStartTime.hour() < 17
                ? "Afternoon"
                : "Evening";

        return {
            startDateTime: thisSlotStartTime,
            endDateTime: thisSlotEndTime,
            section: section,
        } as unknown as CalendarTimeSlot;
    });

    const slotsOfThisDayWithBooking = slotsOfThisDay.map(calendarTimeSlot => {
        const bookedByThisUserTimeSlot = bookedByThisUserTimeSlots?.find(v => inCalendarTimeSlot(calendarTimeSlot, v));
        const bookedTimeSlot = bookedTimeSlots?.find(v => inCalendarTimeSlot(calendarTimeSlot, v));
        return {
            calendarTimeSlot,
            bookedByThisUserTimeSlot,
            bookedTimeSlot,
        };
    });
    
    const slotsOfThisDayWithBookingAndAvailableDuration = slotsOfThisDayWithBooking.map(item => {
        const firstBookedSlot = slotsOfThisDayWithBooking.find(v => v.calendarTimeSlot.startDateTime > item.calendarTimeSlot.startDateTime && (!!v.bookedByThisUserTimeSlot || !!v.bookedTimeSlot))
        const availableDuration = !!firstBookedSlot
            ? firstBookedSlot.calendarTimeSlot.startDateTime.diff(item.calendarTimeSlot.startDateTime, "minutes")
            : endTimeMoment.diff(item.calendarTimeSlot.startDateTime, "minutes");
        return {
            ...item,
            availableDuration: availableDuration,
        };
    });

    const sections = _.chain(slotsOfThisDayWithBookingAndAvailableDuration).groupBy(item => item.calendarTimeSlot.section).map((value, key) => ({ key, items: value })).value();

    return (
        <>
            <Grid container sx={{ paddingTop: 2 }} spacing={1}>
                {sections.map(section => (
                    <React.Fragment key={section.key}>
                        <Grid item xs={12}>
                            <Typography>{t(section.key)}</Typography>
                        </Grid>
                        {section.items.map((item, index) => {
                            return (
                                <Grid item {...xs2sm3md4lg6xl6GridItem} key={item.calendarTimeSlot.startDateTime?.toISOString()}>
                                    <TimeSlot 
                                        calendarTimeSlot={item.calendarTimeSlot} 
                                        bookingOfThisUser={item.bookedByThisUserTimeSlot} 
                                        booking={item.bookedTimeSlot} 
                                        requiredDuration={requiredDuration}
                                        availableDuration={item.availableDuration}
                                        onBookingClick={() => { console.log("onBookingClick") }} />
                                </Grid>
                            )
                        })}
                    </React.Fragment>
                ))}
            </Grid>
        </>
    );
}
