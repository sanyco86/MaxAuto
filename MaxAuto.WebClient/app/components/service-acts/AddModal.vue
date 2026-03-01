<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'

const schema = z.object({
  name: z.string().min(2, 'Слишком коротко')
})

const open = ref(false)

type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({
  name: ''
})

const toast = useToast()
async function onSubmit(event: FormSubmitEvent<Schema>) {
  toast.add({ title: 'Успех', description: `Новый заказ-наряд ${event.data.name} добавлен`, color: 'success' })
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="Заказ-наряд" description="Добавить заказ-наряд">
    <UButton label="Добавить заказ-наряд" icon="i-lucide-plus" />

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
