import { useTranslation } from "react-i18next";

function useDateTimeDisplay() {
    const { t } = useTranslation();

    function handleCopy(text: string) {
        if (typeof text === "string" || typeof text == "number") {
            console.error(
                `copy typeof ${typeof text} to clipboard, must be a string or number.`
            );
        } else {
            console.error(
                `Cannot copy typeof ${typeof text} to clipboard, must be a string or number.`
            );
        }
    }

    return { handleCopy };
}