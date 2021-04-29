<template>
    <v-btn height="100%" block dark :color="GetStatus()" @click="ToggleComponent()" :disabled="CheckSeat()">
        <v-icon>{{ Icon }}</v-icon>
    </v-btn>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class ToggleButton extends Vue {
        @Prop(String) type = this.Type;
        @Prop(Number) index = this.Index;
        @Prop(Number) seat = this.Seat;
        @Prop(String) icon = this.Icon;

        status = false;

        $axios: any;

        GetStatus() {
            if (this.status) {
                return "green";
            }
            return "";
        }

        ToggleComponent() {
            this.Status = !this.Status;
            let type = this.Type;
            let index = this.Index;
            let status = this.Status;
            console.log(type);
            this.$axios
                .post(
                    "http://framework/TOGGLE_COMPONENT",
                    { type, index, status }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        CheckSeat() {
            if (this.type === "engine") {
                if (this.seat === -1) {
                    return false;
                } else {
                    return true;
                }
            } else if (this.type === "door" || this.type === "window") {
                if (this.seat + 1 === this.index) {
                    return false;
                } else if (this.seat === -1 && (this.index === 4 || this.index === 5)) {
                    return false;
                } else if (this.type === "window" && this.seat === -1) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        get Status() {
            return this.status;
        }

        set Status(value: boolean) {
            this.status = value;
        }

        get Type() {
            return this.type;
        }

        set Type(value: string) {
            this.type = value;
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
    }
</script>

<style scoped>
</style>