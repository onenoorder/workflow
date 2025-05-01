import 'dart:ffi';
import 'dart:io';

import 'package:demo_frontend/workflow_bindings.dart';
import 'package:ffi/ffi.dart';


const String _libName = 'workflow';

final DynamicLibrary _dylib = () {
  if (Platform.isMacOS || Platform.isIOS) {
    return DynamicLibrary.open('$_libName.framework/$_libName');
  }
  if (Platform.isAndroid || Platform.isLinux) {
    return DynamicLibrary.open('/home/path/tp/DemoBackend/bin/Release/net8.0/linux-x64/native/DemoBackend.so');
  }
  if (Platform.isWindows) {
    return DynamicLibrary.open('$_libName.dll');
  }
  throw UnsupportedError('Unknown platform: ${Platform.operatingSystem}');
}();

String callExecuteWorkflow(String workflowCode) {
  final bindings = WorkflowBindings(_dylib);

  final workflowCodePtr = workflowCode.toNativeUtf8().cast<Char>();

  final resultPtr = bindings
      .execute_code(
        workflowCodePtr,
      )
      .cast<Utf8>();
  final result = resultPtr.toDartString();
  return result;
}