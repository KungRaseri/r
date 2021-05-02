<template>
    <v-btn :class="{'button-seat-in': Index === Seat, 'button-unavailable': Index !== Seat && Taken}"
           height="100%" block dark @click="ToggleComponent()" :disabled="CheckSeats()">
        <v-icon :size="IconSize()">{{ Icon }}</v-icon>
    </v-btn>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class SeatButton extends Vue {
        @Prop(Number) index = this.Index;
        @Prop(Number) seat = this.Seat;
        @Prop(String) icon = this.Icon;
        @Prop(Boolean) taken = this.Taken;
        @Prop(Number) seats = this.Seats;

        $axios: any;

        ToggleComponent() {
            let index = this.Index;
            let type = "seat";
            this.$axios
                .post(
                    "http://framework/TOGGLE_COMPONENT",
                    { type, index }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        IconSize() {
            let size = window.innerWidth + window.innerHeight;
            return size * 0.01;
        }

        CheckSeats() {
            if (this.Index + 1 < this.Seats) {
                return false;
            }
            return true;
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

        get Seats() {
            return this.seats;
        }

        set Seats(value: number) {
            this.seats = value;
        }
    }
</script>