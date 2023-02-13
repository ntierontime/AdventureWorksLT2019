import * as React from 'react';
import { Platform } from 'react-native';

import type { DrawerNavigationProp } from '@react-navigation/drawer';
import { getHeaderTitle } from '@react-navigation/elements';
import {
    CardStyleInterpolators,
    createStackNavigator,
} from '@react-navigation/stack';
import { Appbar } from 'react-native-paper';

import ExampleList from './ExampleList';
import { StackScreenList } from './StackScreenList';
import ItemPage from './views/BuildVersion/ItemPage';
import { ViewItemTemplates } from './shared/viewModels/ViewItemTemplates';
import IndexPage from './views/BuildVersion/IndexPage';

const Stack = createStackNavigator();

export default function Root() {
    const cardStyleInterpolator =
        Platform.OS === 'android'
            ? CardStyleInterpolators.forFadeFromBottomAndroid
            : CardStyleInterpolators.forHorizontalIOS;
    return (
        <Stack.Navigator
            screenOptions={({ navigation }) => {
                return {
                    detachPreviousScreen: !navigation.isFocused(),
                    cardStyleInterpolator,
                    header: ({ navigation, route, options, back }) => {
                        const title = getHeaderTitle(options, route.name);
                        return (
                            <Appbar.Header elevated>
                                {back ? (
                                    <Appbar.BackAction onPress={() => navigation.goBack()} />
                                ) : (navigation as any).openDrawer ? (
                                    <Appbar.Action
                                        icon="menu"
                                        isLeading
                                        onPress={() =>
                                            (
                                                navigation as any as DrawerNavigationProp<{}>
                                            ).openDrawer()
                                        }
                                    />
                                ) : null}
                                <Appbar.Content title={title} />
                            </Appbar.Header>
                        );
                    },
                };
            }}
        >
            <Stack.Screen
                name="ExampleList"
                component={ExampleList}
                options={{
                    title: 'Examples',
                }}
            />
            {/* {(Object.keys(examples) as Array<keyof typeof examples>).map((id) => {
                return (
                    <Stack.Screen
                        key={id}
                        name={id}
                        component={examples[id]}
                        options={{
                            title: examples[id].title,
                            headerShown: id !== 'themingWithReactNavigation',
                        }}
                    />
                );
            })} */}
            {StackScreenList && StackScreenList.map((oneScreen) => {
                return (
                    <Stack.Screen
                        key={oneScreen.name}
                        name={oneScreen.name}
                        options={{
                            title: oneScreen.name,
                            headerShown: true,
                        }}
                    >
                        {() => { return oneScreen.component}}
                    </Stack.Screen>
                );
            })}
        </Stack.Navigator>
    );
}
