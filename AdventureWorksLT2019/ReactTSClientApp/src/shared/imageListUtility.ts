export const imagesInOneRow = 4;
export const quiltedImageListRowHeight = 250;

export function quiltedImageSrcSet(image: string, size: number, rows = 1, cols = 1) {
    return {
        src: `${image}?w=${size * cols}&h=${size * rows}&fit=crop&auto=format`,
        srcSet: `${image}?w=${size * cols}&h=${size * rows
            }&fit=crop&auto=format&dpr=2 2x`,
    };
}


export function getQuiltedImageRowsAndCol(index: number, totalRowCount: number, itemsInLastRow: number, totalItems: number) {

    const currentImageRowIndex = Math.floor(index / imagesInOneRow);
    const indexInCurrentImageRow = index % imagesInOneRow;
    const evenRowFlag = currentImageRowIndex % 2 === 0;
    if (currentImageRowIndex < (totalRowCount - 1) || itemsInLastRow == 0) {

        if (evenRowFlag) {
            if (indexInCurrentImageRow === 0) {
                // 1st item
                return { rows: 2, cols: 2, };
            }
            else if (indexInCurrentImageRow === 1 || indexInCurrentImageRow === 2) {
                // 2nd and 3rd item
                return { rows: 1, cols: 1, };
            }
            else {
                // 4th
                return { rows: 1, cols: 2, };
            }
        }
        else {
            if (indexInCurrentImageRow === 0) {
                // 1st item
                return { rows: 1, cols: 2, }
            }
            else if (indexInCurrentImageRow === 1) {
                // 2nd item
                return { rows: 2, cols: 2, };
            }
            else {
                // 3rd and 4th item
                return { rows: 1, cols: 1, };
            }
        }
    }
    else {
        if (itemsInLastRow === 1) {
            return { rows: 2, cols: 4, };
        }
        else if (itemsInLastRow === 2) {
            return { rows: 2, cols: 2, };
        }
        else { // 3
            if (indexInCurrentImageRow === 1) {
                // 2nd item
                return { rows: 2, cols: 2, };
            }
            else {
                // 3rd item
                return { rows: 2, cols: 1, };
            }
        }
    }
}
