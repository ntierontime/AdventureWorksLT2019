export function ConvertObjectToList(theObject: Object) {
    return Object.keys(theObject).map(function (key) {
        return {
            key: key,
            value: theObject[key] as unknown as boolean[]
        };
    });
}