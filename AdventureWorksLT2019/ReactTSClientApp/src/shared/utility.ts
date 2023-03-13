import moment from "moment";
import { localStorageRefreshIntervalInHours } from "./constants";

export function ConvertObjectToList(theObject: Object) {
    return Object.keys(theObject).map(function (key) {
        return {
            key: key,
            value: theObject[key] as unknown as boolean[]
        };
    });
}

// savedDateTime field and constants.localStorageRefreshIntervalInHours
export const getLocalStorage = <TData>(localStorageKey: string): TData => {
    const data = JSON.parse(localStorage.getItem(localStorageKey));
    if (!!!data || !!!data.data || !!!data.savedDateTime)
        return null;

    const different = moment.duration(moment().diff(moment(data.savedDateTime)));
    if (different.hours() > localStorageRefreshIntervalInHours) {
        localStorage.removeItem(localStorageKey);
        return null;
    }

    return data.data;
}

export const setLocalStorage = <TData>(localStorageKey: string, data: TData) => {
    localStorage.setItem(localStorageKey, JSON.stringify({ savedDateTime: moment(), data: data }));
}