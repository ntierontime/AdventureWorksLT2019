import React from "react";

export interface INameValuePair {
    name: any;
    value: any;
    selected: boolean;
}

export interface INameValuePairWithIcon {
    name: string;
    value: any;
    icon: React.ReactElement;
}
