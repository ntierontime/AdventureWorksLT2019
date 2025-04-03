export const buildFullUrl = (url: string, routeParams: any, queryStringParams_NonArray: any, queryStringParams_Array: {key: string, values: any[]}[]) => {
    let fullUrl = buildFullUrlWithRoute(url, routeParams);
    let queryStrings = "";
    if (!!queryStringParams_NonArray) {
        queryStrings = convertParametersToQueryString_NonArray(queryStringParams_NonArray);
    }
    if (!!queryStringParams_Array) {
        const queryStrings_Array = queryStringParams_Array.map(item => convertParametersToQueryString_Array(item.key, item.values)).join('&');
        if(!!queryStrings_Array) {
            queryStrings = !!queryStrings && !!queryStrings_Array
                ? queryStrings + "&" + queryStrings_Array
                : queryStrings + queryStrings_Array;
        }
    }
    fullUrl = !!queryStrings ? fullUrl + '?' + queryStrings : fullUrl;
    return fullUrl;
}

export const buildFullUrlWithRoute = (url: string, routeParams: any) => {
    let fullUrl = url;
    if (!!routeParams) {
        const relativeRoute = convertParametersToRoute(routeParams);
        if (!!relativeRoute) {
            fullUrl += '/' + relativeRoute;
        }
    }
    return fullUrl;
}

export const convertParametersToRoute = (params: any): string => {
    // https://morioh.com/p/480aef8e92cd
    // Exclude empty or null or undefined properties or fields.
    // ES 6
    if (!!!params)
        return null;
    return Object.keys(params).filter(key => params[key]).map(key => '' + params[key]).join('/');

    // // ES 5
    // return Object.keys(params).filter(function(key){ return params[key]; }).map(function(key) {
    //   return key + '=' + params[key]
    // }).join('&');
}

export const convertParametersToQueryString_NonArray = (params: any): string => {
    // https://morioh.com/p/480aef8e92cd
    // Exclude empty or null or undefined properties or fields.
    // ES 6
    if (!!!params)
        return null;
    return Object.keys(params).filter(key => params[key]).map(key => key + '=' + encodeURIComponent(params[key])).join('&');

    // // ES 5
    // return Object.keys(params).filter(function(key){ return params[key]; }).map(function(key) {
    //   return key + '=' + params[key]
    // }).join('&');
}

export const convertParametersToQueryString_Array = (key: string, values: any[]): string => {
    // https://morioh.com/p/480aef8e92cd
    // Exclude empty or null or undefined properties or fields.
    // ES 6
    if (!!!values)
        return null;
    return values.map(item => key + '=' + item).join('&');
}