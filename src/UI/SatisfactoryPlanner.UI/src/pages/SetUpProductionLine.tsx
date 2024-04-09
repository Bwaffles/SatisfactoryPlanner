import React, { useState } from "react";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import { ContentLayout } from "components/Layout/ContentLayout";
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormDescription,
  FormMessage,
  Button,
  Input,
} from "components/Elements";
import { useSetUpProductionLine } from "features/productionLines/api/setUpProductionLine";
import { ErrorResponse } from "lib/api";

const formSchema = z.object({
  name: z.string().min(1, { message: "Required." }),
});

export const SetUpProductionLine = () => {
  const [setUpErrorResponse, setSetUpErrorResponse] =
    useState<ErrorResponse | null>(null);
  const navigate = useNavigate();
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
    },
  });
  const setUpProductionLineMutation = useSetUpProductionLine();

  function onSubmit(values: z.infer<typeof formSchema>) {
    setUpProductionLineMutation.mutate(
      {
        data: values,
      },
      {
        onSuccess: () => navigate(`/production-lines`, { replace: true }), // TODO return id of line and go to details page
        onError: (error) => setSetUpErrorResponse(error),
      }
    );
  }
  return (
    <ContentLayout title="Set Up a New Production Line">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
          <FormField
            control={form.control}
            name="name"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Name</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormDescription>
                  This name should describe the main purpose of your production
                  line (e.g. Main Iron Ingots - Line 1).
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          {setUpErrorResponse != null && (
            <div>
              {setUpErrorResponse.messages.map((message) => {
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
          <Button>Set Up</Button>
          <Button
            variant="outline"
            className="ml-3"
            onClick={() => navigate(`/production-lines`)}
          >
            Cancel
          </Button>
        </form>
      </Form>
    </ContentLayout>
  );
};
