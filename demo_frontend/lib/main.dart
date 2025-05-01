import 'package:demo_frontend/workflow.dart';
import 'package:flutter/material.dart';
import 'dart:convert';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Workflow demo',
      theme: ThemeData(primarySwatch: Colors.blue),
      home: Scaffold(
        appBar: AppBar(title: Text('Workflow demo')),
        body: AddColumnsScreen(),
      ),
    );
  }
}

class AddColumnsScreen extends StatefulWidget {
  @override
  _AddColumnsScreenState createState() => _AddColumnsScreenState();
}

class _AddColumnsScreenState extends State<AddColumnsScreen> {
  List<Widget> columns = [
    // Initially, there is only one column
    Column(children: [CodeExecutorScreen()]),
  ];

  void addColumn() {
    setState(() {
      // Add a new column with some items
      columns.add(Column(children: [CodeExecutorScreen()]));
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          children: [
            // First column (always visible)
            Expanded(child: columns[0]),
            // Add the other columns dynamically based on the state
            for (int i = 1; i < columns.length; i++)
              Expanded(child: columns[i]),
          ],
        ),
        ElevatedButton(onPressed: addColumn, child: Text('Add Workflow')),
      ],
    );
  }
}

class CodeExecutorScreen extends StatefulWidget {
  @override
  _CodeExecutorScreenState createState() => _CodeExecutorScreenState();
}

class _CodeExecutorScreenState extends State<CodeExecutorScreen> {
  final TextEditingController _controller = TextEditingController();
  List<String> _outputList = [];
  bool _isLoading = false;

  Future<void> _executeCode() async {
    final code = _controller.text.trim();
    if (code.isEmpty) return;

    setState(() {
      _isLoading = true;
      _outputList = [];
    });

    try {
      // Call your external function
      final jsonString = callExecuteWorkflow(code);
      final List<dynamic> decodedList = jsonDecode(jsonString);
      final List<String> stringList = List<String>.from(decodedList);

      setState(() {
        _outputList = stringList;
      });
    } catch (e) {
      setState(() {
        _outputList = ["Something went wrong"];
      });
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: SizedBox(
        height: 800, // or MediaQuery.of(context).size.height * 0.9
        child: Column(
          children: [
            // Code input
            TextField(
              controller: _controller,
              maxLines: 10,
              decoration: InputDecoration(
                labelText: 'Workflow Code',
                border: OutlineInputBorder(),
              ),
            ),
            SizedBox(height: 16),
            // Execute button
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: _isLoading ? null : _executeCode,
                child:
                    _isLoading
                        ? SizedBox(
                          height: 18,
                          width: 18,
                          child: CircularProgressIndicator(strokeWidth: 2),
                        )
                        : Text('Execute'),
              ),
            ),
            SizedBox(height: 24),
            // Output list
            Flexible(
              child:
                  _outputList.isEmpty
                      ? Center(child: Text('No output yet'))
                      : ListView.builder(
                        itemCount: _outputList.length,
                        itemBuilder: (context, index) {
                          return ListTile(title: Text(_outputList[index]));
                        },
                      ),
            ),
          ],
        ),
      ),
    );
  }
}
