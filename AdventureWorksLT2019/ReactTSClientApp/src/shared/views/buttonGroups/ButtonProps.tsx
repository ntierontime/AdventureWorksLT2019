import { IconButtonPropsSizeOverrides } from "@mui/material";
import { OverridableStringUnion } from "@mui/types";

export interface RouteButtonProps {
    pageUrl: string;
    routeParams?: any;
    queryStringParams?: any;
    size?: OverridableStringUnion<'small' | 'medium' | 'large', IconButtonPropsSizeOverrides>;
}

export interface GenericButtonProps {
    componentAttrs: any;
    size?: OverridableStringUnion<'small' | 'medium' | 'large', IconButtonPropsSizeOverrides>;
}
