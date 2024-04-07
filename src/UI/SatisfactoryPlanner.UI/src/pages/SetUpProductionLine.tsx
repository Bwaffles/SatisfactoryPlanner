import React from "react";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

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
import { useNavigate } from "react-router-dom";

const formSchema = z.object({
  name: z.string().min(1, { message: "Required." }),
});

export const SetUpProductionLine = () => {
  const navigate = useNavigate();
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
    },
  });

  function onSubmit(values: z.infer<typeof formSchema>) {
    // TODO Do something with the form values.
    // ✅ This will be type-safe and validated.
    console.log(values);
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
