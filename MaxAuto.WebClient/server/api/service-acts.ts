import type { ServiceAct } from "~/types/service-act";

const serviceActs: ServiceAct[] = [
  {
    id: 1,
    name: "Антифриз",
    status: "completed"
  },
  {
    id: 2,
    name: "Масло моторное",
    status: "opened"
  },
  {
    id: 3,
    name: "Масло трансмиссионное",
    status: "opened"
  }
]

export default eventHandler(async () => {
  return serviceActs
})
