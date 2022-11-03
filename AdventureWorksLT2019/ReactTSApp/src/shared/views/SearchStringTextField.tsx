import { Divider, MenuItem, Stack, TextField } from "@mui/material";
import { useTranslation } from "react-i18next";
import { TextSearchTypes } from "./TextSearchTypes";

export interface SearchStringTextFieldProps {
    label: string,
    fieldName: string,
    searchTypeFieldName: string,
    searchTypeDefaultValue: TextSearchTypes,
    register: any
}
export function SearchStringTextField(props: SearchStringTextFieldProps) {
    const {register, label, fieldName, searchTypeFieldName, searchTypeDefaultValue} = props;
    const { t } = useTranslation();

    return (
        <Stack direction="row" spacing={0} divider={<Divider orientation="vertical" flexItem />}>
            <TextField
                id={searchTypeFieldName + 'Select'}
                select
                sx={{ minWidth: 150 }}
                name={searchTypeFieldName}
                {...register(searchTypeFieldName)}
                autoComplete={searchTypeFieldName}
                variant="filled"
                size="small"
                defaultValue={searchTypeDefaultValue}
            >
                <MenuItem key={TextSearchTypes.Contains} value={TextSearchTypes.Contains}>{t("Contains")}</MenuItem>
                <MenuItem key={TextSearchTypes.StartsWith} value={TextSearchTypes.StartsWith}>{t("StartsWith")}</MenuItem>
                <MenuItem key={TextSearchTypes.EndsWith} value={TextSearchTypes.EndsWith}>{t("EndsWith")}</MenuItem>
            </TextField>
            <TextField
                label={label}
                name={fieldName}
                {...register(fieldName)}
                autoComplete={fieldName}
                fullWidth
                autoFocus
                variant='filled'
                size="small"
            />
        </Stack>
    );
}