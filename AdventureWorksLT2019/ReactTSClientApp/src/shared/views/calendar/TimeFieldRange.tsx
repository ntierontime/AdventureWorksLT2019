import { useTranslation } from "react-i18next";
import { TimeField } from "@mui/x-date-pickers";
import { InputAdornment, TextField } from "@mui/material";

export interface TimeFieldRangeProps {
    readOnly?: boolean;
    disabled?: boolean;
    label?: string;
    startTime?: any;
    endTime?: any;
    setStartTime?: (newValue: any) => void;
    setEndTime?: (newValue: any) => void;
}

export const TimeFieldRange = (props: TimeFieldRangeProps): JSX.Element => {
    const { t } = useTranslation();
    const { disabled, readOnly, label, startTime, endTime, setStartTime, setEndTime } = props;

    return (
        <TextField
            sx={{ maxWidth: "200px" }}
            disabled={disabled}
            label={t(label)}
            error={startTime >= endTime}
            helperText={startTime >= endTime ? "end time in next day" : ""}
            InputProps={{
                readOnly: true,
                startAdornment: <InputAdornment position="start">
                    <TimeField
                        readOnly={readOnly}
                        disabled={disabled}
                        sx={{ marginRight: -4 }}
                        inputProps={{ style: { textAlign: 'center', } }}
                        InputProps={{ disableUnderline: true, readOnly: readOnly }}
                        variant="standard"
                        value={startTime}
                        onChange={(newValue) => setStartTime(newValue)}
                    />
                </InputAdornment>,
                endAdornment: <InputAdornment position="end">
                    <TimeField
                        readOnly={readOnly}
                        disabled={disabled}
                        sx={{ marginLeft: -4 }}
                        inputProps={{ style: { textAlign: 'center' } }}
                        InputProps={{ disableUnderline: true, readOnly: readOnly }}
                        variant="standard"
                        value={endTime}
                        onChange={(newValue) => setEndTime(newValue)}
                    />
                </InputAdornment>,
            }} />
    );
}
