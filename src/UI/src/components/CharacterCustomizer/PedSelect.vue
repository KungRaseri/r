<template>
    <v-card v-show="Show" class="panel" dark>
        <v-container>
            <v-row dense>
                <v-text-field v-model="Search" :outlined="true" :rounded="true" prepend-inner-icon="mdi-magnify" dense/>
            </v-row>
            <v-row dense>
                <v-col>
                    <v-btn color="red" :rounded="true" block>Back</v-btn>
                </v-col>
                <v-col>
                    <v-btn color="green" :rounded="true" block>Continue</v-btn>
                </v-col>
            </v-row>
            <v-divider class="my-4" />
            <v-row class="mt-1">
                <v-btn v-show="Match(ped)" v-for="ped in Peds" :key="ped" block @click="sendped(ped)">
                    {{ ped }}
                </v-btn>
            </v-row>
        </v-container>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class PedSelect extends Vue {
        @Prop(Boolean) show = this.Show;

        peds: string[];
        search = "";

        filler: string[] = [
            "Test1",
            "Test2",
            "Test3"
        ]

        constructor() {
            super();
            this.peds = this.filler;
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
</style>