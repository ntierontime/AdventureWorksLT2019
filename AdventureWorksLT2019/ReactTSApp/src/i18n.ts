import i18n from 'i18next';
import Backend from 'i18next-http-backend';
import LanguageDetector from 'i18next-browser-languagedetector';
import { initReactI18next } from 'react-i18next';

// We are relying on i18next for Translation when display: 
// 1. number
// 2. double
// 3. currency
// 4. DateTime
// TODO: Steps to Add a new language/locales
// 1. Add a new folder in /public/locales with the language/locales name
// 2. Add a new entry in "supportedLngs" field
// 3. Add a new "if" in "getCurrency(language: string)" method to return currency

export const supportedLngs = ['en', 'es', 'fr'];
export const getCurrency = (language: string): string => {
    if(language === 'en') {
        return "CAD";
    }
    if(language === 'es') {
        return "EUR";
    }
    if(language === 'fr') {
        return "CAD";
    }
    return "CAD"
}

i18n
    // load translation using http -> see /public/locales
    // learn more: https://github.com/i18next/i18next-http-backend
    .use(Backend)
    // detect user language
    // learn more: https://github.com/i18next/i18next-browser-languageDetector
    .use(LanguageDetector)
    // pass the i18n instance to react-i18next.
    .use(initReactI18next)
    // init i18next
    // for all options read: https://www.i18next.com/overview/configuration-options
    .init({
        lng: "en",
        supportedLngs: supportedLngs,
        fallbackLng: 'en',
        lowerCaseLng: true,
        debug: false,
        fallbackNS: false,
        interpolation: {
            escapeValue: false, // not needed for react as it escapes by default
        }
    });

// https://www.i18next.com/translation-function/formatting
export const i18nFormats = {
    dateTime: {
        format: "{{val, datetime}}",
        dateShort: {
            dateStyle: 'short',
        },
        dateLong: {
            dateStyle: 'long',
        },
        dateFull: {
            dateStyle: 'full',
        },
        dateTimeShort: {
            dateStyle: 'short', timeStyle: 'short',
        },
        dateTimeLong: {
            dateStyle: 'long', timeStyle: 'long',
        },
        dateTimeFull: {
            dateStyle: 'full', timeStyle: 'full',
        },
    },
    number: {
        format: "{{val, number}}",
    },
    double: {
        format: "{{val, number(minimumFractionDigits: 2)}}",
    },
    currency: {
        format: "{{val, currency}}",
    },
};

export default i18n;