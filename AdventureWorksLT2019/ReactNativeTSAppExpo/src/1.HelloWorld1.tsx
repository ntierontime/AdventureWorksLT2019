import React from "react";
import { View, Text } from "react-native";
import { useTranslation } from 'react-i18next';

function HelloWorld1() {
    const { t, i18n } = useTranslation();
    return (
        // https://reactnative.dev/docs/view
        <View
            style={{
                flex: 1,
                justifyContent: "center",
                alignItems: "center",
                backgroundColor: "#3498db",
            }}
        >
            {/* https://reactnative.dev/docs/text */}
            <Text
                style={{
                    fontSize: 30,
                    fontWeight: "bold",
                    color: "#fff",
                    textTransform: "uppercase",
                }}
            >
                Hello world! {t('AdvancedSearch')}
            </Text>
        </View>
    );
}

HelloWorld1.title = "HelloWorld1";

export default HelloWorld1;