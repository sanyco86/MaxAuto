<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import type { User } from "~/types/user"

const props = defineProps<{ user: User }>()
const emit = defineEmits<{ (e: 'updated', user: User): void }>()

const open = defineModel<boolean>('open')

const schema = z.object({
  firstName: z.string().min(2, 'Слишком коротко'),
  lastName: z.string().min(2, 'Слишком коротко'),
  email: z.email('Невалидный email'),
  password: z.string().min(6, 'Должен быть не менее 6 символов').optional(),
  position: z.string().optional(),
  role: z.enum(['admin', 'owner', 'manager', 'visitor']),
  workshopId: z.string().optional(),
})

type Schema = z.output<typeof schema>

const state = reactive<Partial<Schema>>({
  firstName: '',
  lastName: '',
  email: '',
  password: '',
  position: '',
  role: 'visitor',
  workshopId: ''
})

watch(
  [() => open.value, () => props.user],
  ([isOpen, w]) => {
    if (!isOpen || !w) return

    state.firstName = w.firstName ?? ''
    state.lastName = w.lastName ?? ''
    state.email = w.email ?? ''
    state.password = w.password ?? ''
    state.position = w.position ?? ''
    state.role = w.role ?? 'visitor'
    state.workshopId = w.workshop?.id ?? ''
  },
  { immediate: true }
)

const toast = useToast()

async function onSubmit(event: FormSubmitEvent<Schema>) {
  const updated = await $fetch<User>(`/api/users/${props.user.id}`, {
    method: 'PUT',
    body: {
      firstName: event.data.firstName,
      lastName: event.data.lastName,
      email: event.data.email,
      password: event.data.password,
      position: event.data.position,
      role: event.data.role,
      workshopId: event.data.workshopId,
    }
  })

  toast.add({
    title: 'Успех',
    description: `Пользователь ${updated.email} успешно обновлен`,
    color: 'success'
  })

  emit('updated', updated)
  open.value = false
}
</script>

<template>
  <UModal v-model:open="open" title="Пользователь" description="Редактировать пользователя">
    <template #body>
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Имя" name="firstName">
          <UInput v-model="state.firstName" class="w-full" />
        </UFormField>
        <UFormField label="Фамилия" name="lastName">
          <UInput v-model="state.lastName" class="w-full" />
        </UFormField>
        <UFormField label="Email" name="email">
          <UInput v-model="state.email" class="w-full" />
        </UFormField>
        <UFormField label="Пароль" name="password">
          <UInput v-model="state.password" class="w-full" />
        </UFormField>
        <UFormField label="Должность" name="position">
          <UInput v-model="state.position" class="w-full" />
        </UFormField>
        <UFormField label="Роль" name="role">
          <UInput v-model="state.role" class="w-full" />
        </UFormField>
        <UFormField label="Сервис" name="workshopId">
          <UInput v-model="state.workshopId" class="w-full" />
        </UFormField>

        <div class="flex justify-end gap-2">
          <UButton label="Отмена" color="neutral" variant="subtle" @click="open = false" />
          <UButton label="Сохранить" color="primary" variant="solid" type="submit" />
        </div>
      </UForm>
    </template>
  </UModal>
</template>
