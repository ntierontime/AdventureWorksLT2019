import { StyleSheet, Platform } from "react-native";

export const PRIMARY_COLOR = "#f39220";
export const PRIMARY_TEXT_COLOR = "#686868";
export const SECONDARY_TEXT_COLOR = "#9a9a9a";

export const ROW = {
    flexDirection: "row" as "row" | "column" | "row-reverse" | "column-reverse",
    justifyContent: "space-between" as "space-between" | "center" | "flex-end" | "flex-start" | "space-around" | "space-evenly"
};

export const itemViewsStyles = StyleSheet.create({
    card: {
        margin: 0,
    },
    row: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        paddingVertical: 8,
        paddingHorizontal: 16,
    },
});