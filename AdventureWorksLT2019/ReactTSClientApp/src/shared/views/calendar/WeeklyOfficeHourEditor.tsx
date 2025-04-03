import React from "react";
import { Button, Checkbox, FormControlLabel, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { Controller, useForm, useFieldArray } from 'react-hook-form';
import { TimePicker } from "@mui/x-date-pickers";

import { useTranslation } from "react-i18next";
import dayjs from "dayjs";

import { WeeklyOfficeHoursSetting, WeekDays, WeekDayOfficeHour } from "./types";

export interface WeeklyOfficeHourEditorProps {
    minutesStep?: number; // minutesStep not working when use up/down arrow keys on keyboard
    readOnly?: boolean;
    setting: WeeklyOfficeHoursSetting;
    onSubmit: (data: WeeklyOfficeHoursSetting) => void;
}

export const WeeklyOfficeHourEditor = (props: WeeklyOfficeHourEditorProps): JSX.Element => {
    const { t } = useTranslation();

    const {
        minutesStep, // minutesStep not working when use up/down arrow keys on keyboard
        readOnly = false,
        setting, //workInHoliday, haveLunchHour, weekDayOfficeHours
        onSubmit,
    } = props;
    // console.log(setting);
    const methods = useForm({
        mode: 'onChange',
        reValidateMode: 'onChange',
        defaultValues: setting,
    });
    const { register, control, setValue, watch, handleSubmit, reset, trigger, formState: { isValid, errors, isDirty } } = methods;
    //const watchWorkInHoliday = watch("workInHoliday", setting.workInHoliday);
    const watchHaveLunchHour = watch("haveLunchHour", setting.haveLunchHour);
    const { fields: weekDayOfficeHours, update: weekDayOfficeHoursUpdate } = useFieldArray({
        control,
        name: "weekDayOfficeHours",
    });

    const onApplyAllDays = (allDays: WeekDayOfficeHour) =>{
        weekDayOfficeHours.map((oneDay, index) => {
            if(oneDay.weekDay !== WeekDays.AllDays) {
                weekDayOfficeHoursUpdate(index, {...allDays, weekDay: oneDay.weekDay, enabled: oneDay.enabled })
            }
        });
    }

    return (
        <Stack direction="column" component="form" noValidate onSubmit={(event) => { event.preventDefault(); event.stopPropagation(); handleSubmit(onSubmit)(event); }} >
            <Stack direction="row" spacing={1}>
                <Controller
                    name='workInHoliday'
                    control={control}
                    render={({ field }) => (
                        <FormControlLabel
                            control={<Checkbox {...field} disabled={readOnly} defaultChecked={setting.workInHoliday} />}
                            label={
                                <Typography>{t('WorkInHoliday')}</Typography>
                            }
                        />
                    )}
                />
                <Controller
                    name='haveLunchHour'
                    control={control}
                    render={({ field }) => (
                        <FormControlLabel
                            control={<Checkbox {...field} disabled={readOnly} defaultChecked={setting.haveLunchHour} />}
                            label={
                                <Typography>{t('HaveLunchHour')}</Typography>
                            }
                        />
                    )}
                />
            </Stack>
            <TableContainer component={Paper}>
                <Table >
                    <TableHead>
                        <TableRow style={{height: 35}}>
                            <TableCell sx={{ minWidth: "20%", maxWidth: "90px" }}></TableCell>
                            <TableCell sx={{ minWidth: "20%", maxWidth: "90px" }} align="right">{t("DayStartTime")}</TableCell>
                            <TableCell sx={{ minWidth: "20%", maxWidth: "90px" }} align="left">{t("DayEndTime")}</TableCell>
                            <TableCell sx={{ minWidth: "20%", maxWidth: "90px" }} align="right">{watchHaveLunchHour ? t("LunchStartTime") : " "}</TableCell>
                            <TableCell sx={{ minWidth: "20%", maxWidth: "90px" }} align="left">{watchHaveLunchHour ? t("LunchEndTime") : " "}</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {weekDayOfficeHours.map((oneDay, index) => {
                            const { weekDay, enabled, label, startTime, endTime, lunchHourStartTime, lunchHourEndTime } = oneDay;
                            const allDayFlag = weekDay === WeekDays.AllDays;
                            const disableUnderline = !enabled || readOnly;
                            return (
                                <React.Fragment key={weekDay}>
                                {(!readOnly || !allDayFlag) && (<TableRow
                                    
                                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                    style={{height: 35}}
                                >
                                    <TableCell component="th" scope="row" sx={{ minWidth: "20%", maxWidth: "90px", padding: 0.5 }}>
                                        {!allDayFlag && <FormControlLabel
                                            control={<Checkbox disabled={readOnly} checked={enabled} onChange={(newValue: any) => { weekDayOfficeHoursUpdate(index, { ...oneDay, enabled: !enabled }) }} />}
                                            label={
                                                <Typography>{t(label)}</Typography>
                                            }
                                        />}
                                        {allDayFlag && <Button variant="contained" size="small" onClick={()=>onApplyAllDays(oneDay)}>{t("ApplyAllDays")}</Button> }
                                    </TableCell>
                                    <TableCell align="right" sx={{ minWidth: "20%", maxWidth: "90px", padding: 0.5 }}>
                                        <TimePicker
                                            minutesStep={minutesStep}
                                            readOnly={readOnly || !enabled}
                                            sx={{ paddingTop: 0.5, paddingRight: 0, paddingLeft: 0, maxWidth: "85px" }}
                                            value={startTime}
                                            onChange={(newValue: any) => { console.log(newValue); weekDayOfficeHoursUpdate(index, { ...oneDay, startTime: dayjs(newValue) }) }}
                                            slotProps={{
                                                textField: {
                                                    variant: "standard",
                                                    inputProps: { style: { textAlign: 'right', } },
                                                    InputProps: { disableUnderline: disableUnderline }
                                                }
                                            }}
                                        />
                                    </TableCell>
                                    <TableCell align="left" sx={{ minWidth: "20%", maxWidth: "90px", padding: 0.5 }}>
                                        <TimePicker
                                            minutesStep={minutesStep}
                                            readOnly={readOnly || !enabled}
                                            sx={{ paddingTop: 0.5, paddingRight: 0, paddingLeft: 0, maxWidth: "85px" }}
                                            value={endTime}
                                            onChange={(newValue: any) => { weekDayOfficeHoursUpdate(index, { ...oneDay, endTime: dayjs(newValue) }) }}
                                            slotProps={{
                                                textField: {
                                                    variant: "standard",
                                                    inputProps: { style: { textAlign: 'left', } },
                                                    InputProps: { disableUnderline: disableUnderline }
                                                }
                                            }}
                                        />
                                    </TableCell>
                                    <TableCell align="right" sx={{ minWidth: "20%", maxWidth: "90px", padding: 0.5 }}>
                                        {watchHaveLunchHour && <TimePicker
                                            minutesStep={minutesStep}
                                            readOnly={readOnly || !enabled}
                                            sx={{ paddingTop: 0.5, paddingRight: 0, paddingLeft: 0, maxWidth: "85px" }}
                                            value={lunchHourStartTime}
                                            onChange={(newValue: any) => { weekDayOfficeHoursUpdate(index, { ...oneDay, lunchHourStartTime: dayjs(newValue) }) }}
                                            slotProps={{
                                                textField: {
                                                    variant: "standard",
                                                    inputProps: { style: { textAlign: 'right', } },
                                                    InputProps: { disableUnderline: disableUnderline }
                                                }
                                            }}
                                        />}</TableCell>
                                    <TableCell align="left" sx={{ minWidth: "20%", maxWidth: "90px", padding: 0.5 }}>
                                        {watchHaveLunchHour && <TimePicker
                                            minutesStep={minutesStep}
                                            readOnly={readOnly || !enabled}
                                            sx={{ paddingTop: 0.5, paddingRight: 0, paddingLeft: 0, maxWidth: "85px" }}
                                            value={lunchHourEndTime}
                                            onChange={(newValue: any) => { weekDayOfficeHoursUpdate(index, { ...oneDay, lunchHourEndTime: dayjs(newValue) }) }}
                                            slotProps={{
                                                textField: {
                                                    variant: "standard",
                                                    inputProps: { style: { textAlign: 'left', } },
                                                    InputProps: { disableUnderline: disableUnderline }
                                                }
                                            }}
                                        />}</TableCell>
                                </TableRow>)}
                                </React.Fragment>
                            )
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
            <Stack direction="row" spacing={1}>
                <Button type="submit">Submit</Button>
            </Stack>
        </Stack>
    );
}
