export const formatNumber = (number: number) => {
    return new Intl.NumberFormat().format(number);
};

export const formatPercent = (number: number) => {
    return new Intl.NumberFormat(undefined, {
        style: "percent",
    }).format(number);
};
