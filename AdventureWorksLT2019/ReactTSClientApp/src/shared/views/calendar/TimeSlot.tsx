import { Button } from "@mui/material";

import DeleteIcon from '@mui/icons-material/Delete';
import EventAvailableIcon from '@mui/icons-material/EventAvailable';

import { i18nFormats } from "src/i18n";
import { useTranslation } from "react-i18next";
import { BookedTimeSlot, CalendarTimeSlot } from "./types";

export interface TimeSlotProps {
    requiredDuration: number;
    availableDuration: number;
    calendarTimeSlot: CalendarTimeSlot;
    bookingOfThisUser?: BookedTimeSlot;
    booking?: BookedTimeSlot;
    onBookingClick?: () => void;
    onCancelClick?: () => void;
}

export const TimeSlot = (props: TimeSlotProps): JSX.Element => {
    const { t } = useTranslation();

    const { requiredDuration, availableDuration, calendarTimeSlot, bookingOfThisUser, booking, onBookingClick, onCancelClick } = props;
    // console.log("calendarTimeSlot bookingOfThisUser booking", calendarTimeSlot, bookingOfThisUser, booking ); 
    const color =
        (!!!booking
            ? !!bookingOfThisUser ? "error" : 
                (requiredDuration <= availableDuration || availableDuration === -1 ? "primary": "warning")
            : "inherit");

    const icon =
        !!!booking
            ? !!bookingOfThisUser ? <DeleteIcon fontSize="small" /> : 
                (requiredDuration <= availableDuration || availableDuration === -1 ? <EventAvailableIcon fontSize="small" /> : null)
            : null;
    
    const onClick =
        !!!booking 
            ? !!bookingOfThisUser ? onCancelClick : 
                (requiredDuration <= availableDuration || availableDuration === -1 ? onBookingClick : null)
            : null;

    return (
        <Button variant="contained" endIcon={icon} onClick={onClick} color={color} sx={{ minWidth: "100%", borderRadius: 15 }} >
            {t(i18nFormats.dateTime.format, { val: calendarTimeSlot.startDateTime, formatParams: { val: i18nFormats.dateTime.timeShort, } })}
        </Button>

    );
}
