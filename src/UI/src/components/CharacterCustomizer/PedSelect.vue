<template>
    <v-slide-x-reverse-transition>
        <v-card v-show="Show" class="panel" dark>
            <v-container class="fill">
                <v-row dense>
                    <v-text-field v-model="Search" :outlined="true" :rounded="true" prepend-inner-icon="mdi-magnify" dense />
                </v-row>
                <v-row dense>
                    <v-col v-if="false">
                        <v-btn color="red" :rounded="true" block>Back</v-btn>
                    </v-col>
                    <v-col>
                        <v-btn color="green" :rounded="true" block @click="sendped(Ped)">Continue</v-btn>
                    </v-col>
                </v-row>
                <v-divider class="my-4" />
                <v-container class="scroll">
                    <v-row v-show="Match(ped)" v-for="ped in Peds" :key="ped">
                        <v-btn block @click="SetPed(ped)">
                            {{ ped }}
                        </v-btn>
                    </v-row>
                </v-container>
            </v-container>
        </v-card>
    </v-slide-x-reverse-transition>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit, Ref } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class PedSelect extends Vue {
        @Ref() readonly virtualScroll!: HTMLTableRowElement;
        @Prop(Boolean) show = this.Show;

        peds = [""]
        ped = "";
        search = "";

        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "LIST_OF_PEDS":
                        this.Peds = e.data._peds;
                        break;
                    default:
                        break;
                }
            });
        }

        Match(value: string) {
            let search = this.Search.toUpperCase();
            let ped = value.toUpperCase();

            if (search !== "") {
                if (ped.includes(search)) {
                    return true;
                } else {
                    return false;
                }
            }

            return true;
        }

        SetPed(value: string) {
            this.Ped = value;
            let ped = value;
            this.$axios
                .post(
                    "http://framework/SET_CHARACTER_MODEL",
                    {
                        ped
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        @Emit()
        sendped(value: string) {
            return value;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }

        get Peds() {
            return this.peds;
        }

        set Peds(value: string[]) {
            this.peds = value;
        }

        get Ped() {
            return this.ped;
        }

        set Ped(value: string) {
            this.ped = value;
        }

        get Search() {
            return this.search;
        }

        set Search(value: string) {
            this.search = value;
        }
    }
</script>

<style scoped>
    .panel {
        position: fixed;
        top: 0;
        right: 0;
        width: 20%;
        height: 100%;
    }

    .fill {
        height: 100%;
    }

    .scroll {
        overflow: scroll;
        height: 87%;
    }
</style>