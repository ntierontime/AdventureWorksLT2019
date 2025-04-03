import { DatePicker } from "@mui/x-date-pickers";
import { Grid, IconButton, Paper, Stack, styled, TextField, Tooltip, Typography } from "@mui/material";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';

import dayjs from "dayjs";
import { i18nFormats } from "src/i18n";
import { useTranslation } from "react-i18next";
import { CalendarDateStatus } from "./types";

export interface WeeklyCalendarHeaderProps {
    disablePast: boolean;
    disableFuture: boolean;
    selectedDay?: string;
    setSelectedDay: (newValue: string) => void;
}

export const WeeklyCalendarHeader = (props: WeeklyCalendarHeaderProps): JSX.Element => {
    const { t } = useTranslation();

    const { disablePast, disableFuture, selectedDay, setSelectedDay } = props;
    const today = dayjs().startOf('day');
    const selectedDayMoment = dayjs(dayjs(selectedDay).startOf('day').toDate());
    const firstDayOfThisWeek = dayjs(selectedDayMoment.toDate()).add(-selectedDayMoment.day(), "days");
    const thisWeek = [...Array(7)].map((_, i) => {
        const thisDay = dayjs(dayjs(firstDayOfThisWeek.toISOString()).add(i, "days"));
        return {
            dateTime: thisDay,
            disabled: (disablePast && thisDay < today) || (disableFuture && thisDay > today),
        } as unknown as CalendarDateStatus;
    });
    const getBackgroundColor = (status: CalendarDateStatus) => {
        if (selectedDayMoment.toISOString() === status.dateTime.toISOString()) {
            return "red";
        }
        return status.disabled ? "gray" : "";
    }

    const handlePreviousWeek = () => {
        const newSelectedDay = dayjs(selectedDay).add(-7, "days");
        if (today < newSelectedDay || !disablePast) {
            setSelectedDay(newSelectedDay.toISOString());
        }
        else {
            setSelectedDay(today.toISOString());
        }
    }

    const handleNextWeek = () => {
        const newSelectedDay = dayjs(selectedDay).add(7, "days");
        if (!disableFuture || today > newSelectedDay) {
            setSelectedDay(newSelectedDay.toISOString());
        }
        else {
            setSelectedDay(today.toISOString());
        }
    }

    const handleSelectADay = (status: CalendarDateStatus) => {
        if (status.disabled)
            return;
        setSelectedDay(status.dateTime.toISOString());
    }
    const onAnyTextFieldChanged = (e: any) => {
        if (!!e?.preventDefault) {
            e?.preventDefault();
            e?.stopPropagation();
        }
    }

    return (
        <>
            <Grid item xs={12}>
                <DatePicker
                    format="MMMM, YYYY"
                    views={['month', 'year', 'day']}
                    value={selectedDayMoment}
                    onChange={(newValue) => {
                        setSelectedDay(newValue.toISOString())
                    }}
                    // remove slotProps to remove customization
                    slotProps={{
                        textField: {
                            onBeforeInput: onAnyTextFieldChanged,
                            InputProps: {
                                size: 'medium',
                                readOnly: true,
                                sx: { fontSize: 30, maxWidth: 225 },
                                disableUnderline: true
                            },
                            variant: 'standard'
                        }
                    }}
                />
            </Grid>
            <Grid item xs={12}>
                <Stack
                    height={75}
                    spacing={0}
                    direction="row"
                    alignItems="center"
                >
                    <Tooltip title={t("PreviousWeek")}>
                        <span>
                            <IconButton aria-label="previousWeek"
                                size="large"
                                sx={{ width: 1 / 16 }}
                                onClick={handlePreviousWeek}
                                disabled={disablePast && today > firstDayOfThisWeek}>
                                <ChevronLeftIcon fontSize="inherit" />
                            </IconButton>
                        </span>
                    </Tooltip>
                    {thisWeek.map(item => (
                        <Item key={item.dateTime.toISOString()}
                            onClick={() => handleSelectADay(item)}
                            sx={{ width: 1 / 8, backgroundColor: getBackgroundColor(item) }}>
                            <Typography variant="subtitle1">{t(i18nFormats.dateTime.format, { val: item.dateTime, formatParams: { val: i18nFormats.dateTime.dateWeekDayOnly, } })}</Typography>
                            <Typography variant="h5">{t(i18nFormats.dateTime.format, { val: item.dateTime, formatParams: { val: i18nFormats.dateTime.dateDayInMonthOnly, } })}</Typography>
                        </Item>
                    ))}
                    <Tooltip title={t("NextWeek")}>
                        <span>
                            <IconButton aria-label="nextWeek"
                                size="large"
                                sx={{ width: 1 / 16 }}
                                onClick={handleNextWeek}
                                disabled={disableFuture && today < thisWeek[6].dateTime}>
                                <ChevronRightIcon fontSize="inherit" />
                            </IconButton>
                        </span>
                    </Tooltip>
                </Stack>
            </Grid>
        </>
    );
}

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    spacing: 0,
    border: 2,
    borderRadius: 0,
    borderColor: theme.palette.info.contrastText,
    color: theme.palette.text.secondary,
    alignItems: 'baseline',
    height: 75,
    lineHeight: '30px',
}));

export const Input = styled(TextField)`
  && {
    .MuiInputBase-root {
      font-size: 24px;
    }
    .MuiInputLabel-root {
      line-height: 30px;
    }
    .Mui-focused {
      line-height: 22px;
    }
  }
`;
