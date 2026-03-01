<script setup lang="ts">
import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'

const profileSchema = z.object({
  firstName: z.string().min(2, 'Слишком коротко'),
  lastName: z.string().min(2, 'Слишком коротко'),
  email: z.email('Невалидный email'),
  password: z.string().min(6, 'Должен быть не менее 6 символов').optional(),
})

type ProfileSchema = z.output<typeof profileSchema>

const profile = reactive<Partial<ProfileSchema>>({
  firstName: 'Benjamin',
  lastName: 'Canac',
  email: 'ben@nuxtlabs.com',
  password: "******"
})

const toast = useToast()
async function onSubmit(event: FormSubmitEvent<ProfileSchema>) {
  toast.add({
    title: 'Успех',
    description: 'Профиль успешно обновлен.',
    icon: 'i-lucide-check',
    color: 'success'
  })
  console.log(event.data)
}

</script>

<template>
  <UDashboardPanel id="profile">
    <template #header>
      <UDashboardNavbar title="Профиль">
        <template #leading>
          <UDashboardSidebarCollapse />
        </template>
      </UDashboardNavbar>
    </template>

    <template #body>
      <div class="flex flex-col gap-4 sm:gap-6 lg:gap-12 w-full lg:max-w-2xl mx-auto">
        <UForm
          id="profile"
          :schema="profileSchema"
          :state="profile"
          @submit="onSubmit"
        >
          <UPageCard
            title="Профиль"
            description="Управление информацией о своем профиле."
            variant="naked"
            orientation="horizontal"
            class="mb-4"
          >
            <UButton
              form="profile"
              label="Сохранить изменения"
              color="neutral"
              type="submit"
              class="w-fit lg:ms-auto"
            />
          </UPageCard>

          <UPageCard variant="subtle">
            <UFormField
              name="firstName"
              label="Имя"
              description="Имя будет отображаться в актах, счетах и другой коммуникации."
              required
              class="flex max-sm:flex-col justify-between items-start gap-4"
            >
              <UInput
                v-model="profile.firstName"
                autocomplete="off"
              />
            </UFormField>
            <UFormField
              name="lastName"
              label="Фамилия"
              description="Фамилия будет отображаться в актах, счетах и другой коммуникации."
              required
              class="flex max-sm:flex-col justify-between items-start gap-4"
            >
              <UInput
                v-model="profile.lastName"
                autocomplete="off"
              />
            </UFormField>
            <USeparator />
            <UFormField
              name="email"
              label="Email"
              required
              class="flex max-sm:flex-col justify-between items-start gap-4"
            >
              <UInput
                v-model="profile.email"
                type="email"
                autocomplete="off"
              />
            </UFormField>
            <USeparator />
            <UFormField
              name="password"
              label="Пароль"
              class="flex max-sm:flex-col justify-between items-start gap-4"
            >
              <UInput
                v-model="profile.password"
                type="password"
                autocomplete="off"
              />
            </UFormField>
          </UPageCard>
        </UForm>
      </div>
    </template>
  </UDashboardPanel>
</template>
