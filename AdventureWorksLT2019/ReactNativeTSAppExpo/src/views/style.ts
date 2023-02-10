import { StyleSheet, Platform } from "react-native";

export const PRIMARY_COLOR = "#f39220";
export const PRIMARY_TEXT_COLOR = "#686868";
export const SECONDARY_TEXT_COLOR = "#9a9a9a";

export const ROW = {
    flexDirection: "row" as "row" | "column" | "row-reverse" | "column-reverse",
    justifyContent: "space-between" as "space-between" | "center" | "flex-end" | "flex-start" | "space-around" | "space-evenly"
};


export const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "#fff",
    },
    content: {
        padding: 20,
    },
    closeButton: {
        alignItems: "flex-end",
        height: 40,
        paddingHorizontal: 15,
    },
    coverImage: {
        width: 200,
        height: 200,
        borderRadius: 20,
        alignSelf: "center",
    },
    songName: {
        fontWeight: "bold",
        fontSize: 20,
        color: PRIMARY_TEXT_COLOR,
        marginTop: 20,
        alignSelf: "center",
    },
    singerName: {
        fontSize: 16,
        color: SECONDARY_TEXT_COLOR,
        marginTop: 5,
        fontStyle: "italic",
        alignSelf: "center",
    },
    progress: {
        margin: 30,
    },
    time: {
        ...ROW,
        marginHorizontal: Platform.OS === "android" ? 15 : 0,
    },
    timeText: {
        color: "#757575",
    },
    slider: {
        height: 30,
    },
    controls: {
        flexDirection: "row",
        justifyContent: "center",
        alignItems: "center",
        marginHorizontal: 30,
    },
    primaryControlIcon: {
        fontSize: 50,
        color: "#f27637",
        marginHorizontal: 25,
    },
    secondaryControlIcon: {
        fontSize: 25,
        color: PRIMARY_TEXT_COLOR,
    },
});