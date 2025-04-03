import { Orientation } from "@mui/material";

export interface WizardPartialProps<TCompositeModel> {
    orientation: Orientation;
    data: TCompositeModel;
}