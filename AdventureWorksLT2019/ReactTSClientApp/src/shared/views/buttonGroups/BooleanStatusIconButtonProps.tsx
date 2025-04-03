import { IconButtonPropsSizeOverrides } from "@mui/material";
import { OverridableStringUnion } from "@mui/types";

export default interface BooleanStatusIconButtonProps {
    identifier: any;
    value: boolean;
    submitting: boolean;
    onChange?: (identifier: any, currentValue:boolean) => void;
    note?: string;
    size?: OverridableStringUnion<'small' | 'medium' | 'large', IconButtonPropsSizeOverrides>;
}