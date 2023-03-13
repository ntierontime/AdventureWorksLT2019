// We are keeping as many data as possible in localStorage, to lower down web api calls, to improve performance.
// key format: {tableName}{webapi method}Data
export enum localStorageKeys {
    ProductModelCompareData = 'ProductModelCompareData',
}
