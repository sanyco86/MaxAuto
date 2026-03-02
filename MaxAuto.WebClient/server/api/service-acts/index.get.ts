import type { ServiceAct } from "~/types/service-act";

const serviceActs: ServiceAct[] = [
  {
    id: 1,
    name: "Антифриз"
  },
  {
    id: 2,
    name: "Масло моторное"
  },
  {
    id: 3,
    name: "Масло трансмиссионное"
  }
]

export default eventHandler(async () => {
  return serviceActs
})
