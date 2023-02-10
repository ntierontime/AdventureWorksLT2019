import React from "react";
import HelloWorld1 from "./1.HelloWorld1";
import IndexPage from "./views/BuildVersion/IndexPage"
// export const EXAMPLE_LIST_OLD = [
//   {
//     name: "Hello World 1",
//     level: 1,
//     component: <HelloWorld1 />,
//   },
//   {
//     name: "Build Version Index Page",
//     level: 1,
//     component: <IndexPage />,
//   }
// ];

import { FlatList } from 'react-native';

import type { StackNavigationProp } from '@react-navigation/stack';
import { Divider, List } from 'react-native-paper';
import { useSafeAreaInsets } from 'react-native-safe-area-context';

import { useExampleTheme } from '.';

export const mainExamples: Record<
  string,
  React.ComponentType<any> & { title: string }
> = {
    helloWorld1: HelloWorld1,    
    buildVersionIndexPage: IndexPage,
}

export const examples: Record<
  string,
  React.ComponentType<any> & { title: string }
> = {
  ...mainExamples,
};

type Props = {
    navigation: StackNavigationProp<{ [key: string]: undefined }>;
  };
  
  type Item = {
    id?: string;
    data?: typeof mainExamples[string];
  };
  
  const data = Object.keys(mainExamples).map(
    (id): Item => ({ id, data: mainExamples[id] })
  );

  export default function ExampleList({ navigation }: Props) {
    const keyExtractor = (item: { id: string }) => item.id;
  
    const { colors, isV3 } = useExampleTheme();
    const safeArea = useSafeAreaInsets();
  
    const renderItem = ({ item }: { item: Item }) => {
      const { data, id } = item;
  
      if (!isV3 && data.title === mainExamples.themingWithReactNavigation.title) {
        return null;
      }
  
      return (
        <List.Item title={data.title} onPress={() => navigation.navigate(id)} />
      );
    };
  
    return (
      <FlatList
        contentContainerStyle={{
          backgroundColor: colors.background,
          paddingBottom: safeArea.bottom,
          paddingLeft: safeArea.left,
          paddingRight: safeArea.right,
        }}
        style={{
          backgroundColor: colors.background,
        }}
        showsVerticalScrollIndicator={false}
        ItemSeparatorComponent={Divider}
        renderItem={renderItem}
        keyExtractor={keyExtractor}
        data={data}
      />
    );
  }
  