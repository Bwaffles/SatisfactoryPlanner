import React from 'react';
import { Button, Card, Dropdown, Header, List } from 'semantic-ui-react';


/**
 * Props
 * =======
 * 
 outputs: [ { id, name, originalItemsPerMinute, itemsPerMinute } ]
 * 
 * */
export class OutputList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            outputs: this.props.outputs.map(output => (
                {
                    ...output,
                    exports: []
                })),
            itemsToExportTo: []
        };

        this.addExport = this.addExport.bind(this);
    }

    componentDidMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', "itemsToExportTo", true);
        xhr.onload = () => {
            const itemsToExportTo = JSON.parse(xhr.responseText);
            this.setState({ itemsToExportTo: itemsToExportTo });
        };
        xhr.send();
    }

    addExport(event, output) {
        console.debug("output", output);

        this.setState(state => {
            return state.outputs.map(currentOutput => {
                if (currentOutput.id == output.id) {
                    currentOutput.exports = [
                        {
                            id: currentOutput.exports.length + 1
                        },
                        ...currentOutput.exports
                    ];
                }

                return currentOutput;
            });
        });
    }

    getItemsToExportToFor(output) {
        return this.state.itemsToExportTo
            .filter(item => item.itemId == output.id);
    }

    render() {

        console.debug("props", this.props);
        console.debug("state", this.state);

        const outputs = this.state.outputs;

        return (
            <Card.Group>
                {outputs.map((output) =>
                    <Card key={output.id} fluid color='green'>
                        <Card.Content>
                            <Card.Header>
                                {output.name}
                            </Card.Header>
                            <Card.Description>
                                {output.itemsPerMinute}/min

                                <Header as='h4' textAlign="left">Export To</Header>

                                <Button onClick={(e) => this.addExport(e, output)}>Add</Button>

                                {output.exports.length > 0 &&
                                    <List>
                                        {output.exports.map((exp) =>

                                            <List.Item key={exp.id} >
                                                <Dropdown placeholder="Select an item to export to">
                                                    <Dropdown.Menu>
                                                        {this.getItemsToExportToFor(output).map((item) =>
                                                            <Dropdown.Item key={item.id} text={item.name} description={item.amount} />
                                                        )}
                                                    </Dropdown.Menu>
                                                </Dropdown>
                                            </List.Item>

                                        )}
                                    </List>
                                }

                            </Card.Description>
                        </Card.Content>
                    </Card>
                )}
            </Card.Group>
        );
    }
}