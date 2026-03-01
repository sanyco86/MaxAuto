<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко'),
  location: z.string().min(2, 'Слишком коротко'),
  address: z.string().optional(),
})
const open = ref(false)

type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({
  name: '',
  location: '',
  address: '',
})

const toast = useToast()
async function onSubmit(event: FormSubmitEvent<Schema>) {
  toast.add({ title: 'Успех', description: `Сервис ${event.data.name} успешно создан`, color: 'success' })
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="Сервис" description="Создать новый сервис">
    <UButton label="Добавить новый сервис" icon="i-lucide-plus" />

    <template #body>
      <UForm
        :schema="schema"
        :state="state"
        class="space-y-4"
        @submit="onSubmit"
      >
        <UFormField label="Название" name="name">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>
        <UFormField label="Местоположение" name="location">
          <UInput v-model="state.location" class="w-full" />
        </UFormField>
        <UFormField label="Адрес" name="address">
          <UInput v-model="state.address" class="w-full" />
        </UFormField>
        <div class="flex justify-end gap-2">
          <UButton
            label="Отмена"
            color="neutral"
            variant="subtle"
            @click="open = false"
          />
          <UButton
            label="Создать"
            color="primary"
            variant="solid"
            type="submit"
          />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
