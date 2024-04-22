import React, { useState } from "react";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import { faPen } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  Button,
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
  Input,
} from "components/Elements";
import { ErrorResponse } from "lib/api";
import { useRenameProductionLine } from "../api/renameProductionLine";

interface ProductionLineNameProps {
  productionLineId: string;
  name: string;
}

const formSchema = z.object({
  name: z.string().min(1, { message: "Name can't be blank." }),
});

export const ProductionLineName = (props: ProductionLineNameProps) => {
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [renameErrorResponse, setRenameErrorResponse] =
    useState<ErrorResponse | null>(null);
  const renameProductionLine = useRenameProductionLine();
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: props.name,
    },
  });

  function onSubmit(values: z.infer<typeof formSchema>) {
    renameProductionLine.mutate(
      {
        productionLineId: props.productionLineId,
        data: values,
      },
      {
        onSuccess: () => setIsEditing(false),
        onError: (error) => setRenameErrorResponse(error),
      }
    );
  }

  return (
    <div className="mb-6 h-10">
      {isEditing ? (
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="flex gap-3">
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => (
                <FormItem className="flex-grow">
                  <FormControl>
                    <Input {...field} />
                  </FormControl>
                  <FormMessage />
                  {renameErrorResponse != null && (
                    <div>
                      {renameErrorResponse.messages.map((message) => {
                        return (
                          <p
                            key={message}
                            className="text-sm font-medium text-destructive-error"
                          >
                            {message}
                          </p>
                        );
                      })}
                    </div>
                  )}
                </FormItem>
              )}
            />
            <Button
              variant="default"
              size="sm"
              isLoading={renameProductionLine.isLoading}
            >
              Rename
            </Button>
            <Button
              variant="ghost"
              size="sm"
              onClick={() => setIsEditing(false)}
            >
              Cancel
            </Button>
          </form>
        </Form>
      ) : (
        <div className="flex items-center gap-3">
          <h1 className="scroll-m-20 text-2xl font-extrabold tracking-tight lg:text-3xl">
            {props.name}
          </h1>
          <Button
            variant="ghost"
            size="icon"
            className="text-muted-foreground"
            onClick={() => {
              form.reset({ name: props.name });
              setRenameErrorResponse(null);
              setIsEditing(true);
            }}
          >
            <FontAwesomeIcon icon={faPen} />
          </Button>
        </div>
      )}
    </div>
  );
};
