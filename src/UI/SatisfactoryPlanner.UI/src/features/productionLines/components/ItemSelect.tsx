import React, { Fragment } from "react";

import {
  Select,
  SelectValue,
  SelectItem,
  SelectContent,
  SelectTrigger,
  SelectGroup,
  SelectLabel,
  SelectSeparator,
} from "components/Elements/Select";
import { useGetItemsToProcess } from "../api/getItemsToProcess";

interface ItemSelectProps {
  onItemSelected?(itemId: string): void;
}

export const ItemSelect = (props: ItemSelectProps) => {
  const { data: response } = useGetItemsToProcess();

  const categories = response!.items.reduce<Record<string, any>>(
    (group, item) => {
      const { category } = item;
      group[category.name] = group[category.name] ?? [];
      group[category.name].push(item);

      return group;
    },
    {}
  );
  const categoryKeys = Object.keys(categories);

  return (
    <Select
      onValueChange={(value) => {
        if (props.onItemSelected) props.onItemSelected(value);
      }}
    >
      <SelectTrigger className="w-[225px]">
        <SelectValue placeholder="Item" />
      </SelectTrigger>
      <SelectContent>
        {categoryKeys.map((category, index) => {
          return (
            <Fragment key={category}>
              <SelectGroup>
                <SelectLabel>{category}</SelectLabel>
                {categories[category].map((item: any) => {
                  return (
                    <SelectItem key={item.id} value={item.id}>
                      {item.name}
                    </SelectItem>
                  );
                })}
              </SelectGroup>
              {index < categoryKeys.length - 1 && (
                <SelectSeparator></SelectSeparator>
              )}
            </Fragment>
          );
        })}
      </SelectContent>
    </Select>
  );
};
