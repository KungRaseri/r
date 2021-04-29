<template>
    <v-btn :class="{'button-seat-in': Index === Seat, 'button-seat': Index !== Seat && Taken}" height="100%" block dark @click="ToggleComponent()">
        <v-icon>{{ Icon }}</v-icon>
    </v-btn>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class SeatButton extends Vue {
        @Prop(Boolean) status = this.Status;
        @Prop(Number) index = this.Index;
        @Prop(Number) seat = this.Seat;
        @Prop(String) icon = this.Icon;
        @Prop(Boolean) taken = this.Taken;

        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "VEHICLE_PANEL_DATA":
                        if (e.data._type === "seat") {
                            this.Status = e.data._status;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        ToggleComponent() {
            this.Status = !this.Status;
            let status = this.Status;
            let type = "seat";
            this.$axios
                .post(
                    "http://framework/TOGGLE_COMPONENT",
                    { type, status }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        get Status() {
            return this.status;
        }

        set Status(value: boolean) {
            this.status = value;
        }

        get Index() {
            return this.index;
        }

        set Index(value: number) {
            this.index = value;
        }

        get Seat() {
            return this.seat;
        }

        set Seat(value: number) {
            this.seat = value;
        }

        get Icon() {
            return this.icon;
        }

        set Icon(value: string) {
            this.icon = value;
        }

        get Taken() {
            return this.taken;
        }

        set Taken(value: boolean) {
            this.taken = value;
        }
    }
</script>

<style scoped>
    .button-seat-in {
        pointer-events: none;
        background-color: rgb(0 128 0 / 0.53) !important;
    }

    .button-seat {
        pointer-events: none;
        background-color: rgb(128 0 0 / 0.53) !important;
    }
</style>