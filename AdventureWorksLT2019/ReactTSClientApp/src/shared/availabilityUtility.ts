// usage: load all compare results from api, can be filtered by some parameters, the result should be filtered by indexes of the record.   
export function availabilityFilteredByIndexes(availability: Object, indexes: number[]) {
    const array = ConvertObjectToArray(availability);
    //console.log(array);
    const arrayFiltered = array?.map((item) => {
        return {
            key: item.key as unknown as string,
            value: (item.value).filter((v, vindex) => indexes?.some(i => i === vindex))
        };
    })?.filter(t => t?.value?.some(t1 => t1));
    //console.log(arrayFiltered);
    return Object.fromEntries(arrayFiltered.map(item => [item.key, item.value]));
}

export function ConvertObjectToArray(theObject: Object) {
    return Object.keys(theObject).map(function (key) {
        return {
            key: key,
            value: theObject[key as keyof Object] as unknown as boolean[]
        };
    });
}
